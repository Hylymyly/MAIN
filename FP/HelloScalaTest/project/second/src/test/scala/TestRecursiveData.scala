package exercise2
import org.scalatest.FunSuite
class TestRecursiveData extends FunSuite{
  val a = Cons(1, Cons(2, Cons(3, Cons(4, Nil()))))
  test("testListIntEmpty: Nil() ")
  {
    val p = RecursiveData.testListIntEmpty(Nil())
    assert(p == true)
  }
  test("testListIntHead: 1 2 3 4 ")
  {
    val p = RecursiveData.testListIntEmpty(Nil())
    assert(p == true)
  }
}
