#include <iostream>
#include <vector>

using namespace std;

int countFives(int number) {
    int count = 0;

    while (number > 0) {
        if (number % 10 == 5) {
            count++;
        }
        number /= 10;
    }

    return count;
}

bool shouldGoBefore(int first, int second) {
    int firstFives = countFives(first);
    int secondFives = countFives(second);

    if (firstFives != secondFives) {
        return firstFives > secondFives;
    }

    return first > second;
}

void insertionSort(vector<int>& numbers) {
    for (int i = 1; i < (int)numbers.size(); i++) {
        int current = numbers[i];
        int j = i - 1;

        while (j >= 0 && shouldGoBefore(current, numbers[j])) {
            numbers[j + 1] = numbers[j];
            j--;
        }

        numbers[j + 1] = current;
    }
}

int main() {
    int n;
    cin >> n;

    vector<int> numbers(n);
    for (int i = 0; i < n; i++) {
        cin >> numbers[i];
    }

    insertionSort(numbers);

    for (int i = 0; i < n; i++) {
        cout << numbers[i] << ' ';
    }

    return 0;
}

/*
Пример входных данных:
5
5 70 15 51335 5255

Пример выходных данных:
5255 51335 15 5 70

Сложность:
по времени: O(n^2), потому что используется сортировка вставками;
по памяти: O(1), если не считать исходный массив.
*/
