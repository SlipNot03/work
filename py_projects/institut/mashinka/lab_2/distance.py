import math 
import numpy as np

class EuclideanDistance:
    def calculate(self,metriks1,metriks2) -> float:
        self._validate_metriks(metriks1, metriks2)

        
        #Складываем квадраты разностей по каждому признаку
        distance = 0.0
        for num1,num2 in zip(metriks1,metriks2):
            distance += (num2 - num1) ** 2

        return math.sqrt(distance)



    
    def _validate_metriks(self,data1,data2):
            
        if not isinstance(data1, np.ndarray) or not isinstance(data2, np.ndarray):
            raise TypeError("data1 и data2 должны быть numpy массивами")
        
        if data1.ndim != 1 or data2.ndim != 1:
            raise ValueError("нарушена одномерность")

        if len(data1) != len(data2):
            raise ValueError("data1 и data2 должны быть одинаковой длины")

        if not np.issubdtype(data1.dtype, np.number):
            raise TypeError("data1 должен содержать числа")
            
        if not np.issubdtype(data2.dtype, np.number):
            raise TypeError("data2 должен содержать числа")


