object FirstLab {
  def main(args: Array[String]): Unit = {
    ChetNe()
  }
  def idealNum(): Unit = {
    var a = Array[Int](1,6,2,3,4,5,28,7)
    val i=0
    val j=0
    var max = 1
    var r = for (i <- 0 until a.length if(tf(a(i))) == true) yield a(i)
    print(r.sorted.last)
  }

  def tf(pop: Int): Boolean={
  var a = for(i <- 1 to pop/2 if pop%i==0) yield i
    if (a.sum==pop) return true
    else return false
  }

  def ChetNe(): Unit ={
    var a = Array[Int](1,2,3,4,5,6,7,7,9,0)
    var ch = for (i <- 0 until a.length if (i%2==0)) yield a(i)
    var nech = for (j <- 0 until a.length if (j%2!=0)) yield a(j)
      println(ch)
      println(nech)
  }

}
