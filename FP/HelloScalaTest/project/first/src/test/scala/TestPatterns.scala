package exercise1
import org.scalatest.FunSuite
class TestPatterns extends FunSuite {
  test("testIntToString: 1")
  {
    val p = Patterns.testToString(1)
    assert(p == "it is one")
  }
  test("testIntToString: 2")
  {
    val p = Patterns.testToString(2)
    assert(p == "it is two")
  }
  test("testIntToString: 3")
  {
    val p = Patterns.testToString(3)
    assert(p == "it is three")
  }
  test("testIntToString: 4")
  {
    val p = Patterns.testToString(4)
    assert(p == "what's that")
  }
  test("testIsMaxAndMoritz: max")
  {
    val p = Patterns.testIsMaxAndMoritz("max")
    assert(p==true)
  }
  test("testIsMaxAndMoritz: Max")
  {
    val p = Patterns.testIsMaxAndMoritz("Max")
    assert(p==true)
  }
  test("testIsMaxAndMoritz: moritz")
  {
    val p = Patterns.testIsMaxAndMoritz("moritz")
    assert(p==true)
  }
  test("testIsMaxAndMoritz: morit"){
    val p = Patterns.testIsMaxAndMoritz("morit")
    assert(p == false)
  }
  test("testIsEven: 1"){
    val p = Patterns. testIsEven(4)
    assert(p == true)
  }
  test("testIsEven: 3"){
    val p = Patterns. testIsEven(3)
    assert(p == false)
  }
  test("testWinsA: Rock, Rock"){
    val p = Patterns.testWinsA(Patterns.Rock, Patterns.Rock)
    assert(p == Patterns.Draw)
  }
  test("testWinsA: Paper, Rock"){
    val p = Patterns.testWinsA(Patterns.Paper, Patterns.Rock)
    assert(p == Patterns.Win)
  }
  test("testWinsA: Scissor, Rock"){
    val p = Patterns.testWinsA(Patterns.Scissor, Patterns.Rock)
    assert(p == Patterns.Lose)
  }
  test("testExtractMammalWeight:KIT and Plants"){
    val p = Patterns.testExtractMammalWeight(Patterns.Mammal("KIT",Patterns.Plants,2))
    assert(p==2)
  }
  test("testUpdateFood: Fish and Plants"){
    val p = Patterns.testUpdateFood(Patterns.Fish("Fish",Patterns.Meat))
    assert(p == Patterns.Fish("Fish",Patterns.Plants))
  }
}

