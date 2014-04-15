﻿namespace OpenStack.Threading
{
    using System.Threading.Tasks;

    /// <summary>
    /// Provides static methods to create completed <see cref="Task"/> and <see cref="Task{TResult}"/> instances.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public static class CompletedTask
    {
        /// <summary>
        /// Gets a completed <see cref="Task"/>.
        /// </summary>
        /// <value>A completed <see cref="Task"/>.</value>
        public static Task Default
        {
            get
            {
                return CompletedTaskHolder.Default;
            }
        }

        /// <summary>
        /// Gets a completed <see cref="Task{TResult}"/> with the specified result.
        /// </summary>
        /// <typeparam name="TResult">The task result type.</typeparam>
        /// <param name="result">The result of the completed task.</param>
        /// <returns>A completed <see cref="Task{TResult}"/>, whose <see cref="Task{TResult}.Result"/> property returns the specified <paramref name="result"/>.</returns>
        public static Task<TResult> FromResult<TResult>(TResult result)
        {
            TaskCompletionSource<TResult> completionSource = new TaskCompletionSource<TResult>();
            completionSource.SetResult(result);
            return completionSource.Task;
        }

        /// <summary>
        /// Gets a canceled <see cref="Task"/>.
        /// </summary>
        /// <returns>A canceled <see cref="Task"/>.</returns>
        public static Task Canceled()
        {
            return CanceledTaskHolder.Default;
        }

        /// <summary>
        /// Gets a canceled <see cref="Task{TResult}"/>.
        /// </summary>
        /// <typeparam name="TResult">The task result type.</typeparam>
        /// <returns>A canceled <see cref="Task{TResult}"/>.</returns>
        public static Task<TResult> Canceled<TResult>()
        {
            return CanceledTaskHolder<TResult>.Default;
        }

        private static class CompletedTaskHolder
        {
            public static readonly Task Default;

            static CompletedTaskHolder()
            {
                Default = CompletedTaskHolder<object>.Default;
            }
        }

        private static class CompletedTaskHolder<T>
        {
            public static readonly Task<T> Default;

            static CompletedTaskHolder()
            {
                TaskCompletionSource<T> completionSource = new TaskCompletionSource<T>();
                completionSource.SetResult(default(T));
                Default = completionSource.Task;
            }
        }

        private static class CanceledTaskHolder
        {
            public static readonly Task Default;

            static CanceledTaskHolder()
            {
                Default = CanceledTaskHolder<object>.Default;
            }
        }

        private static class CanceledTaskHolder<T>
        {
            public static readonly Task<T> Default;

            static CanceledTaskHolder()
            {
                TaskCompletionSource<T> completionSource = new TaskCompletionSource<T>();
                completionSource.SetCanceled();
                Default = completionSource.Task;
            }
        }
    }
}
