using Ninject;
using System;
using TransactionalDbContext;

namespace TstFxtr
{
    public class TestFixtureBuilder
    {
        private IKernel _kernel;
        private ReadCommittedDbContext _context;

        private TestFixtureBuilder() { }

        public TestFixtureBuilder Begin()
        {
            return new TestFixtureBuilder();
        }

        public TestFixtureBuilder WithKernel(IKernel kernel)
        {
            _kernel = kernel;
            return this;
        }

        public TestFixtureBuilder WithContext(ReadCommittedDbContext context)
        {
            _context = context;
            return this;
        }

        public TestFixture Build()
        {
            if (_kernel == null || _context == null) { throw new TestFixtureBuilderException("Please specify a Kernel and Context."); }
            return new TestFixture(_kernel, _context);
        }
    }
    public class TestFixtureBuilderException : Exception
    {
        public TestFixtureBuilderException(string message) : base(message)
        {

        }
    }
}
