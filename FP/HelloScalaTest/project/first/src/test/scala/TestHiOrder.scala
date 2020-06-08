package exercise1
import org.scalatest.FunSuite
class TestHiOrder extends FunSuite {
  test("testNTimes: + 1,2,3")
  {
    val plus: (Int, Int) => Int = _+_
    val p = HiOrder.testNTimes(plus,1,2,3)
    assert(p == 9)
  }
  test("testAnonymousNTimes: 3,2,1")
  {
    val p = HiOrder.testAnonymousNTimes(3,2,1)
    assert(p == 3)
  }
}
