namespace OpenStackNet.Testing.Unit
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenStack.Threading;

    [TestClass]
    public class TaskBlockTests
    {
        protected static readonly string ArgumentExceptionMessage = "Test Message!";
        protected static readonly string DisposeExceptionMessage = "Exception in Dispose";

        #region Using Block with Return

        [TestMethod]
        [Timeout(1500)]
        public async Task TestUsingBlock()
        {
            DisposableObject disposableObject = new DisposableObject(false);
            Func<Task<IDisposable>> acquire =
                () => CompletedTask.FromResult<IDisposable>(disposableObject);
            Func<Task<IDisposable>, Task<int>> body =
                resourceTask => CompletedTask.FromResult(3);
            int result = await CoreTaskExtensions.Using(acquire, body);
            Assert.AreEqual(3, result);
            Assert.AreEqual(1, disposableObject.DisposeCount);
        }

        [TestMethod]
        [Timeout(1500)]
        public async Task TestUsingBlockNullResource()
        {
            Func<Task<IDisposable>> acquire =
                () => CompletedTask.FromResult(default(IDisposable));
            Func<Task<IDisposable>, Task<int>> body =
                resourceTask => CompletedTask.FromResult(3);
            int result = await CoreTaskExtensions.Using(acquire, body);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        [Timeout(1500)]
        public async Task TestUsingBlockExceptionInBody()
        {
            DisposableObject disposableObject = new DisposableObject(false);
            Func<Task<IDisposable>> acquire =
                () => CompletedTask.FromResult<IDisposable>(disposableObject);
            Func<Task<IDisposable>, Task<int>> body =
                resourceTask =>
                {
                    throw new ArgumentException(ArgumentExceptionMessage);
                };

            try
            {
                await CoreTaskExtensions.Using(acquire, body);
                Assert.Fail("Expected an exception.");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(ArgumentExceptionMessage, ex.Message);
                Assert.AreEqual(1, disposableObject.DisposeCount);
            }
        }

        [TestMethod]
        [Timeout(1500)]
        public async Task TestUsingBlockExceptionInDispose()
        {
            DisposableObject disposableObject = new DisposableObject(true);
            Func<Task<IDisposable>> acquire =
                () => CompletedTask.FromResult<IDisposable>(disposableObject);
            Func<Task<IDisposable>, Task<int>> body =
                resourceTask => CompletedTask.FromResult(3);

            try
            {
                await CoreTaskExtensions.Using(acquire, body);
                Assert.Fail("Expected an exception.");
            }
            catch (InvalidOperationException ex)
            {
                Assert.AreEqual(DisposeExceptionMessage, ex.Message);
                Assert.AreEqual(1, disposableObject.DisposeCount);
            }
        }

        [TestMethod]
        [Timeout(1500)]
        public async Task TestUsingBlockExceptionInBodyAndDispose()
        {
            DisposableObject disposableObject = new DisposableObject(true);
            Func<Task<IDisposable>> acquire =
                () => CompletedTask.FromResult<IDisposable>(disposableObject);
            Func<Task<IDisposable>, Task<int>> body =
                resourceTask =>
                {
                    throw new ArgumentException(ArgumentExceptionMessage);
                };

            try
            {
                await CoreTaskExtensions.Using(acquire, body);
                Assert.Fail("Expected an exception.");
            }
            catch (InvalidOperationException ex)
            {
                Assert.AreEqual(DisposeExceptionMessage, ex.Message);
                Assert.AreEqual(1, disposableObject.DisposeCount);
            }
            catch (ArgumentException)
            {
                Assert.Fail("Incorrect exception was propagated to the caller.");
            }
        }

        #endregion

        #region Using Block without Return

        [TestMethod]
        [Timeout(1500)]
        public async Task TestVoidUsingBlock()
        {
            DisposableObject disposableObject = new DisposableObject(false);
            Func<Task<IDisposable>> acquire =
                () => CompletedTask.FromResult<IDisposable>(disposableObject);
            Func<Task<IDisposable>, Task> body =
                resourceTask => CompletedTask.Default;
            await CoreTaskExtensions.Using(acquire, body);
            Assert.AreEqual(1, disposableObject.DisposeCount);
        }

        [TestMethod]
        [Timeout(1500)]
        public async Task TestVoidUsingBlockNullResource()
        {
            Func<Task<IDisposable>> acquire =
                () => CompletedTask.FromResult(default(IDisposable));
            Func<Task<IDisposable>, Task> body =
                resourceTask => CompletedTask.Default;
            await CoreTaskExtensions.Using(acquire, body);
        }

        [TestMethod]
        [Timeout(1500)]
        public async Task TestVoidUsingBlockExceptionInBody()
        {
            DisposableObject disposableObject = new DisposableObject(false);
            Func<Task<IDisposable>> acquire =
                () => CompletedTask.FromResult<IDisposable>(disposableObject);
            Func<Task<IDisposable>, Task> body =
                resourceTask =>
                {
                    throw new ArgumentException(ArgumentExceptionMessage);
                };

            try
            {
                await CoreTaskExtensions.Using(acquire, body);
                Assert.Fail("Expected an exception.");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(ArgumentExceptionMessage, ex.Message);
                Assert.AreEqual(1, disposableObject.DisposeCount);
            }
        }

        [TestMethod]
        [Timeout(1500)]
        public async Task TestVoidUsingBlockExceptionInDispose()
        {
            DisposableObject disposableObject = new DisposableObject(true);
            Func<Task<IDisposable>> acquire =
                () => CompletedTask.FromResult<IDisposable>(disposableObject);
            Func<Task<IDisposable>, Task> body =
                resourceTask => CompletedTask.Default;

            try
            {
                await CoreTaskExtensions.Using(acquire, body);
                Assert.Fail("Expected an exception.");
            }
            catch (InvalidOperationException ex)
            {
                Assert.AreEqual(DisposeExceptionMessage, ex.Message);
                Assert.AreEqual(1, disposableObject.DisposeCount);
            }
        }

        [TestMethod]
        [Timeout(1500)]
        public async Task TestVoidUsingBlockExceptionInBodyAndDispose()
        {
            DisposableObject disposableObject = new DisposableObject(true);
            Func<Task<IDisposable>> acquire =
                () => CompletedTask.FromResult<IDisposable>(disposableObject);
            Func<Task<IDisposable>, Task> body =
                resourceTask =>
                {
                    throw new ArgumentException(ArgumentExceptionMessage);
                };

            try
            {
                await CoreTaskExtensions.Using(acquire, body);
                Assert.Fail("Expected an exception.");
            }
            catch (InvalidOperationException ex)
            {
                Assert.AreEqual(DisposeExceptionMessage, ex.Message);
                Assert.AreEqual(1, disposableObject.DisposeCount);
            }
            catch (ArgumentException)
            {
                Assert.Fail("Incorrect exception was propagated to the caller.");
            }
        }

        #endregion

        #region While Block Simple Condition

        [TestMethod]
        [Timeout(1500)]
        public async Task TestWhileBlock()
        {
            int count = 0;
            Func<bool> condition =
                () =>
                {
                    count++;
                    return count <= 3;
                };

            int iterations = 0;
            Func<Task> body =
                () =>
                {
                    iterations++;
                    return CompletedTask.Default;
                };

            await CoreTaskExtensions.While(condition, body);
            Assert.AreEqual(4, count);
            Assert.AreEqual(3, iterations);
        }

        [TestMethod]
        [Timeout(1500)]
        public async Task TestWhileBlockExceptionInCondition()
        {
            int count = 0;
            Func<bool> condition =
                () =>
                {
                    count++;
                    throw new ArgumentException(ArgumentExceptionMessage);
                };

            int iterations = 0;
            Func<Task> body =
                () =>
                {
                    iterations++;
                    return CompletedTask.Default;
                };

            try
            {
                await CoreTaskExtensions.While(condition, body);
                Assert.Fail("Expected an exception");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(1, count);
                Assert.AreEqual(0, iterations);
                Assert.AreEqual(ArgumentExceptionMessage, ex.Message);
            }
        }

        [TestMethod]
        [Timeout(1500)]
        public async Task TestWhileBlockExceptionInBodyEval()
        {
            int count = 0;
            Func<bool> condition =
                () =>
                {
                    count++;
                    return count <= 3;
                };

            int iterations = 0;
            Func<Task> body =
                () =>
                {
                    iterations++;
                    throw new ArgumentException(ArgumentExceptionMessage);
                };

            try
            {
                await CoreTaskExtensions.While(condition, body);
                Assert.Fail("Expected an exception");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(1, count);
                Assert.AreEqual(1, iterations);
                Assert.AreEqual(ArgumentExceptionMessage, ex.Message);
            }
        }

        [TestMethod]
        [Timeout(1500)]
        public async Task TestWhileBlockExceptionInBody()
        {
            int count = 0;
            Func<bool> condition =
                () =>
                {
                    count++;
                    return count <= 3;
                };

            int iterations = 0;
            Func<Task> body =
                () =>
                {
                    iterations++;
                    return CompletedTask.Default.Select(
                        task =>
                        {
                            throw new ArgumentException(ArgumentExceptionMessage);
                        });
                };

            try
            {
                await CoreTaskExtensions.While(condition, body);
                Assert.Fail("Expected an exception");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(1, count);
                Assert.AreEqual(1, iterations);
                Assert.AreEqual(ArgumentExceptionMessage, ex.Message);
            }
        }

        #endregion

        #region While Block Task Condition

        [TestMethod]
        [Timeout(1500)]
        public async Task TestWhileTaskBlock()
        {
            int count = 0;
            Func<Task<bool>> condition =
                () =>
                {
                    count++;
                    return CompletedTask.FromResult(count <= 3);
                };

            int iterations = 0;
            Func<Task> body =
                () =>
                {
                    iterations++;
                    return CompletedTask.Default;
                };

            await CoreTaskExtensions.While(condition, body);
            Assert.AreEqual(4, count);
            Assert.AreEqual(3, iterations);
        }

        [TestMethod]
        [Timeout(1500)]
        public async Task TestWhileTaskBlockExceptionInConditionEval()
        {
            int count = 0;
            Func<Task<bool>> condition =
                () =>
                {
                    count++;
                    throw new ArgumentException(ArgumentExceptionMessage);
                };

            int iterations = 0;
            Func<Task> body =
                () =>
                {
                    iterations++;
                    return CompletedTask.Default;
                };

            try
            {
                await CoreTaskExtensions.While(condition, body);
                Assert.Fail("Expected an exception");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(1, count);
                Assert.AreEqual(0, iterations);
                Assert.AreEqual(ArgumentExceptionMessage, ex.Message);
            }
        }

        [TestMethod]
        [Timeout(1500)]
        public async Task TestWhileTaskBlockExceptionInCondition()
        {
            int count = 0;
            Func<Task<bool>> condition =
                () =>
                {
                    count++;
                    return CompletedTask.Default.Select<bool>(
                        task =>
                        {
                            throw new ArgumentException(ArgumentExceptionMessage);
                        });
                };

            int iterations = 0;
            Func<Task> body =
                () =>
                {
                    iterations++;
                    return CompletedTask.Default;
                };

            try
            {
                await CoreTaskExtensions.While(condition, body);
                Assert.Fail("Expected an exception");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(1, count);
                Assert.AreEqual(0, iterations);
                Assert.AreEqual(ArgumentExceptionMessage, ex.Message);
            }
        }

        [TestMethod]
        [Timeout(1500)]
        public async Task TestWhileTaskBlockExceptionInBodyEval()
        {
            int count = 0;
            Func<Task<bool>> condition =
                () =>
                {
                    count++;
                    return CompletedTask.FromResult(count <= 3);
                };

            int iterations = 0;
            Func<Task> body =
                () =>
                {
                    iterations++;
                    throw new ArgumentException(ArgumentExceptionMessage);
                };

            try
            {
                await CoreTaskExtensions.While(condition, body);
                Assert.Fail("Expected an exception");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(1, count);
                Assert.AreEqual(1, iterations);
                Assert.AreEqual(ArgumentExceptionMessage, ex.Message);
            }
        }

        [TestMethod]
        [Timeout(1500)]
        public async Task TestWhileTaskBlockExceptionInBody()
        {
            int count = 0;
            Func<Task<bool>> condition =
                () =>
                {
                    count++;
                    return CompletedTask.FromResult(count <= 3);
                };

            int iterations = 0;
            Func<Task> body =
                () =>
                {
                    iterations++;
                    return CompletedTask.Default.Select(
                        task =>
                        {
                            throw new ArgumentException(ArgumentExceptionMessage);
                        });
                };

            try
            {
                await CoreTaskExtensions.While(condition, body);
                Assert.Fail("Expected an exception");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(1, count);
                Assert.AreEqual(1, iterations);
                Assert.AreEqual(ArgumentExceptionMessage, ex.Message);
            }
        }

        #endregion

        protected sealed class DisposableObject : IDisposable
        {
            private readonly bool _throwExceptionInDispose;
            private int _disposeCount;

            public DisposableObject(bool throwExceptionInDispose)
            {
                _throwExceptionInDispose = throwExceptionInDispose;
            }

            public int DisposeCount
            {
                get
                {
                    return _disposeCount;
                }
            }

            public void Dispose()
            {
                _disposeCount++;
                if (_throwExceptionInDispose)
                    throw new InvalidOperationException(DisposeExceptionMessage);
            }
        }
    }
}
