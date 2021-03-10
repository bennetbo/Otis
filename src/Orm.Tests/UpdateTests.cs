﻿using NUnit.Framework;
using Orm.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Orm.Tests
{
  public class UpdateTests : DbTest
  {
    private const int ID = 1276945904;
    private const string NAME_BEFORE = "Alex";
    private const string NAME_AFTER = "Jack";

    [Test]
    public void UpdateTest()
    {
      Connection.Delete<TestData>(q => q.Where(a => a.Id == ID));
      Connection.Insert(Create());
      var entity = Connection.Query<TestData>(q => q.Where(a => a.Id == ID)).FirstOrDefault();
      entity.Name = NAME_AFTER;

      Connection.Update(entity, q => q.Where(d => d.Id == entity.Id));

      var entityAfter = Connection.Query<TestData>(q => q.Where(a => a.Id == ID)).FirstOrDefault();
      Assert.That(entityAfter.Name == NAME_AFTER);
    }

    [Test]
    public async Task UpdateAsyncTest()
    {
      await Connection.DeleteAsync<TestData>(q => q.Where(a => a.Id == ID));
      await  Connection.InsertAsync(Create());
      var entities = await Connection.QueryAsync<TestData>(q => q.Where(a => a.Id == ID));
      var entity = entities.FirstOrDefault();
      entity.Name = NAME_AFTER;

      await Connection.UpdateAsync(entity, q => q.Where(d => d.Id == entity.Id));

      var entitiesAfter = await Connection.QueryAsync<TestData>(q => q.Where(a => a.Id == ID));
      Assert.That(entitiesAfter.First().Name == NAME_AFTER);
    }

    private static TestData Create()
    {
      return new TestData
      {
        Id = ID,
        Datetime = DateTime.Now,
        LongggText = "asdkjaskdjiklasjdlasdklaskldjkladjfg klsjlf",
        Name = NAME_BEFORE,
        Number = 406790904
      };
    }
  }
}