namespace OpenStackNet.Testing.Unit
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using net.openstack.Core;

    [TestClass]
    public class TaskBlockTests
    {
        protected static readonly string ArgumentExceptionMessage = "Test Message!";
        protected static readonly string DisposeExceptionMessage = "Exception in Dispose";

        #region Using Block with Return

        [TestMethod]
        public async Task TestUsingBlock()
        {
            DisposableObject disposableObject = new DisposableObject(false);
            Func<Task<IDisposable>> acquire =
                () => InternalTaskExtensions.CompletedTask<IDisposable>(disposableObject);
            Func<Task<IDisposable>, Task<int>> body =
                resourceTask => InternalTaskExtensions.CompletedTask(3);
            int result = await CoreTaskExtensions.Using(acquire, body);
            Assert.AreEqual(3, result);
            Assert.AreEqual(1, disposableObject.DisposeCount);
        }

        [TestMethod]
        public async Task TestUsingBlockNullResource()
        {
            Func<Task<IDisposable>> acquire =
                () => InternalTaskExtensions.CompletedTask(default(IDisposable));
            Func<Task<IDisposable>, Task<int>> body =
                resourceTask => InternalTaskExtensions.CompletedTask(3);
            int result = await CoreTaskExtensions.Using(acquire, body);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public async Task TestUsingBlockExceptionInBody()
        {
            DisposableObject disposableObject = new DisposableObject(false);
            Func<Task<IDisposable>> acquire =
                () => InternalTaskExtensions.CompletedTask<IDisposable>(disposableObject);
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
        public async Task TestUsingBlockExceptionInDispose()
        {
            DisposableObject disposableObject = new DisposableObject(true);
            Func<Task<IDisposable>> acquire =
                () => InternalTaskExtensions.CompletedTask<IDisposable>(disposableObject);
            Func<Task<IDisposable>, Task<int>> body =
                resourceTask => InternalTaskExtensions.CompletedTask(3);

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
        public async Task TestUsingBlockExceptionInBodyAndDispose()
        {
            DisposableObject disposableObject = new DisposableObject(true);
            Func<Task<IDisposable>> acquire =
                () => InternalTaskExtensions.CompletedTask<IDisposable>(disposableObject);
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
        public async Task TestVoidUsingBlock()
        {
            DisposableObject disposableObject = new DisposableObject(false);
            Func<Task<IDisposable>> acquire =
                () => InternalTaskExtensions.CompletedTask<IDisposable>(disposableObject);
            Func<Task<IDisposable>, Task> body =
                resourceTask => InternalTaskExtensions.CompletedTask();
            await CoreTaskExtensions.Using(acquire, body);
            Assert.AreEqual(1, disposableObject.DisposeCount);
        }

        [TestMethod]
        public async Task TestVoidUsingBlockNullResource()
        {
            Func<Task<IDisposable>> acquire =
                () => InternalTaskExtensions.CompletedTask(default(IDisposable));
            Func<Task<IDisposable>, Task> body =
                resourceTask => InternalTaskExtensions.CompletedTask();
            await CoreTaskExtensions.Using(acquire, body);
        }

        [TestMethod]
        public async Task TestVoidUsingBlockExceptionInBody()
        {
            DisposableObject disposableObject = new DisposableObject(false);
            Func<Task<IDisposable>> acquire =
                () => InternalTaskExtensions.CompletedTask<IDisposable>(disposableObject);
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
        public async Task TestVoidUsingBlockExceptionInDispose()
        {
            DisposableObject disposableObject = new DisposableObject(true);
            Func<Task<IDisposable>> acquire =
                () => InternalTaskExtensions.CompletedTask<IDisposable>(disposableObject);
            Func<Task<IDisposable>, Task> body =
                resourceTask => InternalTaskExtensions.CompletedTask();

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
        public async Task TestVoidUsingBlockExceptionInBodyAndDispose()
        {
            DisposableObject disposableObject = new DisposableObject(true);
            Func<Task<IDisposable>> acquire =
                () => InternalTaskExtensions.CompletedTask<IDisposable>(disposableObject);
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
