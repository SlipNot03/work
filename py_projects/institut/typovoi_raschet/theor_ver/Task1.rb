require_relative "Function/distribution"
require_relative "Function/interval"
require_relative "Function/gistogramm"
require_relative "Function/func_distribution"
require_relative "Function/graf_func_distr"


# Задание 1. По данным выборки требуется:
# - составить статистическое распределение выборки, предварительно записав дискретный вариационный ряд;
# - составить ряд распределения относительных частот;
# - построить полигон частот;
# - составить эмпирическую функцию распределения;
# - построить график эмпирической функции распределения.
# Вариант 1: данные о получении прибыли рядом акционерных обществ района за год (млн руб.).

#Я составляю частоты, относительные частоты, эмпирическую функцию и графики

def main
  # data - выборка
  data = [320, 288, 306, 300, 250, 260, 270, 250, 300, 305, 320, 250, 300, 270, 255]
  puts "p1"
  Distribution.main(data)
  puts "p2"
  Interval.main(data)
  puts "p3"
  Gistogramm.main(data)
  puts "p4"
  FuncDistribution.main(data)
  puts "p5"
  GrafFuncDistr.main(data)
end

main if __FILE__ == $PROGRAM_NAME
