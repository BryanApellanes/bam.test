namespace Bam.Test;

public class AsyncTestCase<T> : TestCase<T>
{
    readonly Func<T, Task> _asyncTestMethod = null!;
    readonly Func<T, TestCaseRegistry, Task> _altAsyncTestMethod = null!;

    readonly Func<T, Task<object>> _outputAsyncTestMethod = null!;
    readonly Func<T, TestCaseRegistry, Task<object>> _altOutputAsyncTestMethod = null!;
    
    internal AsyncTestCase(TestCaseRegistry testCaseRegistry, string testDescription) : base(testCaseRegistry, testDescription)
    {
    }

    public AsyncTestCase(TestCaseRegistry testCaseRegistry, string testDescription, Func<T, Task> asyncTest) : this(
        testCaseRegistry, testDescription)
    {
        this._asyncTestMethod = asyncTest;
    }
    
    public AsyncTestCase(TestCaseRegistry testCaseRegistry, string testDescription, Func<T, TestCaseRegistry, Task> altAsyncTestMethod) : this(
        testCaseRegistry, testDescription)
    {
        this._altAsyncTestMethod = altAsyncTestMethod;
    }

    public AsyncTestCase(TestCaseRegistry testCaseRegistry, string testDescription, Func<T, Task<object>> outputAsyncTestMethod) : this(
        testCaseRegistry, testDescription)
    {
        this._outputAsyncTestMethod = outputAsyncTestMethod;
    }
    
    public AsyncTestCase(TestCaseRegistry testCaseRegistry, string testDescription, Func<T, TestCaseRegistry, Task<object>> altOutputAsyncTestMethod) : this(
        testCaseRegistry, testDescription)
    {
        this._altOutputAsyncTestMethod = altOutputAsyncTestMethod;
    }
    
    bool? _run;
    readonly object _runLock = new object();
    /// <summary>
    /// Causes the test case to run, same as TheTest.
    /// </summary>
    public override TestCase<T> It
    {
        get
        {
            lock (_runLock)
            {
                if (_run == null || _run == false)
                {
                    _run = true;
                    try
                    {
                        T objectUnderTest = _testCaseRegistry.Get<T>();
                        if (objectUnderTest == null)
                        {
                            throw new InvalidOperationException($"Failed to instantiate ObjectUnderTest of type {typeof(T).Name}");
                        }

                        List<Task> tasks = new List<Task>();
                        if (_asyncTestMethod != null)
                        {
                            tasks.Add(_asyncTestMethod(objectUnderTest));
                        }

                        if (_altAsyncTestMethod != null)
                        {
                            tasks.Add(_altAsyncTestMethod(objectUnderTest, _testCaseRegistry));
                        }

                        Task<object>? outputTask = null;
                        Task<object>? altOutputTask = null;
                        if (_outputAsyncTestMethod != null)
                        {
                            outputTask = _outputAsyncTestMethod(objectUnderTest);
                            tasks.Add(outputTask);
                        }

                        if (_altOutputAsyncTestMethod != null)
                        {
                            altOutputTask = _altOutputAsyncTestMethod(objectUnderTest, _testCaseRegistry);
                            tasks.Add(altOutputTask);
                        }

                        Task.WaitAll(tasks.ToArray());

                        if (outputTask != null)
                        {
                            _because.Result = outputTask.Result;
                        }

                        if (altOutputTask != null)
                        {
                            _because.Result = altOutputTask.Result;
                        }
                        
                        _testCaseBecause = new Because<T>(_because, this);
                        _testCaseRegistry.ObjectUnderTest = objectUnderTest;
                    }
                    catch (Exception ex)
                    {
                        Exception = ex;
                        if (!_shouldThrow)
                        {
                            _because.ExceptionWasThrown(ex);
                        }
                        _testCaseBecause = new Because<T>(_because, this);
                    }
                }    
            }
                
            return this;
        }
    }
}