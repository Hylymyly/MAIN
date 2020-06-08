package exercise1

object HiOrder {
  val plus: (Int, Int) => Int = _+_
  val multiply: (Int, Int) => Int = _*_

  def nTimes (f: (Int,Int) => Int,a: Int, b:Int, n:Int): Int = {
    return n* f(a,b);
  }

  def testNTimes(f: (Int,Int) => Int,a: Int, b:Int, n:Int): Int = nTimes(f,a,b,n)

  val sw = (a:Int, b:Int) => if(a>b) a else b

  def testAnonymousNTimes(a: Int, b: Int, n: Int): Int = nTimes(sw,a,b,n)

}
