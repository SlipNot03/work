require_relative "plotter"


# Тут подготавливаются точки для графика эмпирической функции распределения и передаются в общий построитель графиков.


module GrafFuncDistr
  module_function

  def main(data)
    #вызываю Python-рисовалку для построения графика эмпирической функции
    Plotter.ecdf(data)
  end
end
