package exercise1
import org.scalatest.FunSuite
class TestFunctions extends FunSuite{
  test("testCircle: 2")
  {
    val p = Functions.testCircle(2)
    assert(p == 4*Math.PI)
  }
  test("testRectangleCurried: 2 and 3")
  {
    val p = Functions.testRectangleCurried(2,3)
    assert(p == 6)
  }
  test("testRectangleUc: 2 and 3")
  {
    val p = Functions.testRectangleUc(2,3)
    assert(p == 6)
  }
}
