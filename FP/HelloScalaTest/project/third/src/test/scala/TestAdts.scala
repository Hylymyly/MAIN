package exercise3
import org.scalatest.FunSuite

import scala.util.Success
class TestAdts extends FunSuite{

  test("testGetNth ")
  {
    val p = Adts.testGetNth(List(1,9,3,2,1,4),2)
    assert(p == Some(3))
  }
  test("testDouble: 5")
  {
    val p = Adts.testDouble(Some(5))
    assert(p == Some(10))
  }
  test("testIsEven: 4")
  {
    val p = Adts.testIsEven(4)
    assert(p == Right(4))
  }
  test("testIsEven: 7")
  {
    val p = Adts.testIsEven(7)
    assert(p == Left("Нечетное число"))
  }
  test("testSafeDivide: 5 на 0")
  {
    val p = Adts.testSafeDivide(5,0)
    assert(p == Left("Вы не можете делить на ноль."))
  }
  test("testGoodOldJava: 2153")
  {
    var parse = (x: String) => Integer.parseInt(x)
    val p = Adts.testGoodOldJava(parse,"2153")
    assert(p == Success(2153) )
  }
}
