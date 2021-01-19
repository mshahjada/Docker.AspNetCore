using CloudApp.Infra;
using Microsoft.EntityFrameworkCore;
using System;


namespace XUnitTest.Mock
{
    public class MockDBContext: AppDBContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            }
        }
    }
}
