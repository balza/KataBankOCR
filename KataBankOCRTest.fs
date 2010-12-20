#light
namespace KataBanlOCR

open System
open NUnit.Framework

[<TestFixture>]
type FileTest() = 
  let mutable file = File()

  /// This is not necessary since the mutable testableClass is already set
  /// but is just for showing how to define a test fixture setup method.
  [<TestFixtureSetUp>]
  member ft.TestFixtureSetUp () = file <- File()

  [<Test>]
  member ft.ParseTest () =  Assert.AreEqual("a", file.Parse())

  [<Test>]
  member ft.ParseTestFail () =  Assert.AreEqual("xixixixa", file.Parse())
