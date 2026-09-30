class EpanechnikovKernel:
#Класс-реализация ядра Епанечникова
    def calculate(self,r):
        self._validate_r(r)
        if abs(r) <= 1:
            return 0.75 * (1 - r ** 2)
        else:
            return 0.0

    def _validate_r(self,r):
        if not isinstance(r,(int,float)):
            raise TypeError("r должен быть числом")