using System;

namespace Dnd.Ddd.Common.Guard
{
    /// <summary>
    ///     Static class used to perform assertions.
    /// </summary>
    public static class Guard
    {
        /// <summary>
        ///     Specifies <typeparamref name="TException"/> which will be thrown when assertion fails.
        /// </summary>
        /// <typeparam name="TException">Type of exception to be thrown upon assertion failure.</typeparam>
        /// <returns>Internal implementation of <see cref="IGuardContext{TException}"/>, used to verify assertion.</returns>
        public static IGuardContext<TException> With<TException>()
            where TException : Exception =>
            new GuardContext<TException>();

        /// <summary>
        ///     Verifies assertion.
        /// </summary>
        /// <typeparam name="TException">Type of exception to be thrown upon assertion failure.</typeparam>
        private class GuardContext<TException> : IGuardContext<TException>
            where TException : Exception
        {
            /// <summary>
            ///     Verifies assertion.
            /// </summary>
            /// <param name="expression">Boolean expression to be verified.</param>
            /// <param name="message">Optional exception message.</param>
            /// <remarks>If <paramref name="expression"/>is true, assertion fails.</remarks>
            /// <exception cref="TException">Thrown when assertion fails.</exception>
            public void Against(bool expression, string message = null)
            {
                if (expression)
                {
                    throw (TException)Activator.CreateInstance(typeof(TException), message ?? "Assertion failed!");
                }
            }
        }
    }
}