#include <iostream>
#include <vector>
#include <map>

using namespace std;

int main() {
    ios::sync_with_stdio(false);
    cin.tie(nullptr);

    int n;
    cin >> n;

    vector<int> a(n);
    int k = 0;

    for (int i = 0; i < n; i++) {
        cin >> a[i];
        if (a[i] > k) {
            k = a[i];
        }
    }

    map<int, int> countInWindow;
    int different = 0;
    int left = 0;

    int bestLeft = 0;
    int bestRight = n - 1;
    int bestLength = n;

    for (int right = 0; right < n; right++) {
        countInWindow[a[right]]++;
        if (countInWindow[a[right]] == 1) {
            different++;
        }

        while (different == k && left <= right) {
            int currentLength = right - left + 1;

            if (currentLength < bestLength) {
                bestLength = currentLength;
                bestLeft = left;
                bestRight = right;
            }

            countInWindow[a[left]]--;
            if (countInWindow[a[left]] == 0) {
                different--;
            }
            left++;
        }
    }

    cout << bestLeft + 1 << ' ' << bestRight + 1;

    return 0;
}

/*
Пример входных данных:
10
1 1 2 1 2 2 3 2 1 3

Пример выходных данных:
7 9

Сложность:
по времени: O(n log k), потому что обращения к map занимают O(log k);
по памяти: O(n + k), массив и счетчики чисел в текущем окне.
*/
