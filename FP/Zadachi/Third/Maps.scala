/*object Maps {

  case class User(name: String,age: Int)

  def main(args: Array[String]): Unit = {
    var userer: Map[String,User] = Map("1"->User("Kir",34),"2"->User("Yir",35),"1"->User("Mir",36))
    print(testNumberFrodos(userer))
  }
  def testGroupUsers(users: Seq[User]): Map[String,Int] = {
    var m: Map[String,Int] = Map()
    var nam = users.groupBy(_.name)
    for (e <- nam){
      var aver = e._2.toBuffer.foldLeft[Int](0)((a,next)=>a+next.age)/e._2.toBuffer.size
      m+=(e._1->aver)
    }
    m
  }

  def testNumberFrodos(map: Map[String, User]): Int =  {
    var c: Int =0;
    for (num <- map) if (num._2.name.indexOf("Adam")!=(-1)) c+=1;
    c
  }

  def testUnderaged(map: Map[String,User]): Map[String,User] = map.filter(k=>k._2.age>=35)
}*/