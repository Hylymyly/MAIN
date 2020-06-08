package exercise2
import org.scalatest.FunSuite
class TestRecursiveFunc extends FunSuite{
  val a = Cons(1, Cons(2, Cons(3, Cons(4, Nil()))))
  val b = Cons(4, Cons(3, Cons(2, Cons(1, Nil()))))
  val c = Cons(Cons(4, Cons(2, Nil())), Cons(Cons(5, Cons(4, Nil())), Nil()))
  val de = (l: List[Int]) => l
  val sw = (x: Int) => x.toDouble
  test("testReverse: 1 2 3 4 ")
  {
    val p = RecursiveFunctions.testReverse(a)
    assert(p == Cons(4, Cons(3, Cons(2, Cons(1, Nil())))))
  }
  test("testMap: 1 2 3 4 ")
  {
    val p = RecursiveFunctions.testMap(a,sw)
    assert(p == Cons(1.0, Cons(2.0, Cons(3.0, Cons(4.0, Nil())))))
  }
  test("testAppend: 1 2 3 4 and 4 3 2 1 ")
  {
    val p = RecursiveFunctions.testAppend(a,b)
    assert(p == Cons(1, Cons(2, Cons(3, Cons(4, Cons(4, Cons(3,Cons(2,Cons(1, Nil())))))))))
  }
  test("testFlatMap:  [4,2] and [5,4] ")
  {
    val p = RecursiveFunctions.testFlatMap(c,de)
    assert(p ==  Cons(2, Cons(4, Cons(4, Cons(5, Nil())))))
  }
}
