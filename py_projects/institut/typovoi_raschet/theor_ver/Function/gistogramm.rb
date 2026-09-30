require_relative "plotter"


# Тут подготавливаются данные для гистограммы или полигона частот и вызывается построение графика через Python.


module Gistogramm
  module_function

  def main(data)
    #Python-рисовалку через matplotlib.
    Plotter.histogram(data)
  end
end
