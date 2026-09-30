import numpy as np
from classifier import ParzenWindowClassifier

class LeaveOneOutValidator:
    def count_errors(self, X, y, k):
        predictions = []
        for i in range(len(X)):
            #создаем отдельные numpy-массивы без проверяемого объекта
            X_without = np.delete(X, i, axis=0)
            y_without = np.delete(y, i)
            
            classifier = ParzenWindowClassifier(k)
            classifier.fit(X_without, y_without)

            #заполняем массив результатами лоо
            predictions.append(classifier.predict_one(X[i]))

        errors = 0
        #здесь проверяем результаты лоо с истинными значениями
        for i in range(len(y)):
            if predictions[i] != y[i]:
                errors += 1
        return errors
    
    def find_best_k(self, X, y):
        errors_by_k = {}
        for k in range(1, len(X) - 1):
            errors = self.count_errors(X, y, k)
            errors_by_k[k] = errors
        best_k = min(sorted(errors_by_k), key=errors_by_k.get)
        return best_k,errors_by_k