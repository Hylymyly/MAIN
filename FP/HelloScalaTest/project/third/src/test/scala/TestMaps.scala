package exercise3
import exercise3.Maps.User
import org.scalatest.FunSuite
class TestMaps extends FunSuite{
  test("testGroupUsers ")
  {
    val p = Maps.testGroupUsers(Seq(User("YarikAdam",20),User("Adam",26),User("Adam",65)))
    assert(p == Map(("YarikAdam",20),("Adam",45)))
  }
  test("testNumberFrodos")
  {
    val p = Maps.testNumberFrodos(Map("1"->User("Adam",34),"2"->User("Yir",35),"3"->User("Mir",36)))
    assert(p == 1)
  }
  test("testUnderaged")
  {
    val p = Maps.testUnderaged(Map("1"->User("Adam",34),"2"->User("Yir",36),"3"->User("Mir",36)))
    assert(p == Map("2"->User("Yir",36),"3"->User("Mir",36)))
  }
}
