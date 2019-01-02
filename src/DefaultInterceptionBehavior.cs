﻿using System;
using Unity.Configuration;
using Unity.Interception.ContainerIntegration.ObjectBuilder;
using Unity.Interception.InterceptionBehaviors;

namespace Unity.Interception.ContainerIntegration
{
    /// <summary>
    /// An injection member that lets you specify behaviors that should
    /// apply to all instances of a type in the container regardless
    /// of what name it's resolved under.
    /// </summary>
    public class DefaultInterceptionBehavior : InterceptionBehaviorBase, IDefaultPolicy
    {
        /// <summary>
        /// Create a new <see cref="DefaultInterceptionBehavior"/> that will
        /// supply the given interception behavior to the container.
        /// </summary>
        /// <param name="interceptionBehavior">Behavior to apply to this type.</param>
        public DefaultInterceptionBehavior(IInterceptionBehavior interceptionBehavior)
            : base(interceptionBehavior)
        {
        }

        /// <summary>
        /// Create a new <see cref="DefaultInterceptionBehavior"/> that will
        /// resolve the given type/name pair to get the behavior.
        /// </summary>
        /// <param name="behaviorType">Type of behavior.</param>
        /// <param name="name">Name for behavior registration.</param>
        public DefaultInterceptionBehavior(Type behaviorType, string name)
            : base(behaviorType, name)
        {
        }

        /// <summary>
        /// Create a new <see cref="DefaultInterceptionBehavior"/> that will
        /// resolve the given type to get the behavior.
        /// </summary>
        /// <param name="behaviorType">Type of behavior.</param>
        public DefaultInterceptionBehavior(Type behaviorType)
            : base(behaviorType)
        {
        }

        /// <summary>
        /// GetOrDefault the list of behaviors for the current type so that it can be added to.
        /// </summary>
        /// <param name="policies">Policy list.</param>
        /// <returns>An instance of <see cref="InterceptionBehaviorsPolicy"/>.</returns>
        protected override InterceptionBehaviorsPolicy GetBehaviorsPolicy<TPolicySet>(ref TPolicySet policies)
        {
            var policy = policies.Get(typeof(IInterceptionBehaviorsPolicy));
            if (!(policy is InterceptionBehaviorsPolicy))
            {
                policy = new InterceptionBehaviorsPolicy();
                policies.Set(typeof(IInterceptionBehaviorsPolicy), policy);
            }
            return (InterceptionBehaviorsPolicy)policy;
        }
    }

    /// <summary>
    /// A generic version of <see cref="DefaultInterceptionBehavior"/> so you
    /// can give the behavior type using generic syntax.
    /// </summary>
    /// <typeparam name="TBehavior">Type of the behavior object to apply.</typeparam>
    public class DefaultInterceptionBehavior<TBehavior> : DefaultInterceptionBehavior
        where TBehavior : IInterceptionBehavior
    {
        /// <summary>
        /// Construct a new <see cref="DefaultInterceptionBehavior{TBehavior}"/> instance
        /// that use the given type and name to resolve the behavior object.
        /// </summary>
        /// <param name="name">Name of the registration.</param>
        public DefaultInterceptionBehavior(string name)
            : base(typeof(TBehavior), name)
        {
        }

        /// <summary>
        /// Construct a new <see cref="DefaultInterceptionBehavior{TBehavior}"/> instance
        /// that uses the given type to resolve the behavior object.
        /// </summary>
        public DefaultInterceptionBehavior()
            : base(typeof(TBehavior))
        {
        }
    }
}
