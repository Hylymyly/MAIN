name:="HelloScalaTest"
version:="1.0"
scalaVersion:="2.13.2"
libraryDependencies += "org.scalatest"%%"scalatest"%"3.0.8"%Test

val common = Seq(scalaVersion:="2.13.2")
lazy val root = project.in(file(".")).aggregate(first,second,third,fourth)
lazy val first = project.in(file("project\\first")).settings(common,libraryDependencies += "org.scalatest"%%"scalatest"%"3.0.8"%Test)
lazy val second = project.in(file("project\\second")).settings(common,libraryDependencies += "org.scalatest"%%"scalatest"%"3.0.8"%Test)
lazy val third = project.in(file("project\\third")).settings(common,libraryDependencies += "org.scalatest"%%"scalatest"%"3.0.8"%Test)
lazy val fourth = project.in(file("project\\fourth")).settings(common,libraryDependencies += "org.scalatest"%%"scalatest"%"3.0.8"%Test)
