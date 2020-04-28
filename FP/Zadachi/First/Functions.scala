package exercise1
object Functions {
 def ploshad(radius: Double): Double = {
  return Math.pow(radius,2)*Math.PI;
 }

 def testCircle(r:Double): Double = ploshad(r)

 def rectangleCurried(a: Double)(b: Double): Double = {
  return a * b;
 }

 def testRectangleCurried(a: Double,b: Double): Double = rectangleCurried(a)(b)

 def ploshadPryam(a: Double, b:Double): Double = {
   return a*b;
 }
 def testRectangleUc(a: Double, b: Double): Double = ploshadPryam(a,b)
}
