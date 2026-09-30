#include <iostream>
#include <map>
#include <string>
#include <limits>
#include <cctype>

using namespace std;

int main() {
    ios::sync_with_stdio(false);
    cin.tie(nullptr);

    int n;
    cin >> n;

    map<string, string> changes;

    for (int i = 0; i < n; i++) {
        string oldName, newName;
        cin >> oldName >> newName;
        changes[oldName] = newName;
    }

    cin.ignore(numeric_limits<streamsize>::max(), '\n');

    string word;
    char c;

    while (cin.get(c)) {
        if (isspace((unsigned char)c)) {
            if (!word.empty()) {
                if (changes.count(word) > 0) {
                    cout << changes[word];
                } else {
                    cout << word;
                }
                word.clear();
            }
            cout << c;
        } else {
            word += c;
        }
    }

    if (!word.empty()) {
        if (changes.count(word) > 0) {
            cout << changes[word];
        } else {
            cout << word;
        }
    }

    return 0;
}

/*
Пример входных данных:
2
Ivanov Petrov
Smirnov Popov
Ivanov is responsible for the conference hall
Smirnov is responsible for the negotiating table

Пример выходных данных:
Petrov is responsible for the conference hall
Popov is responsible for the negotiating table

Сложность:
по времени: O((n + w) log n), где w - количество слов в тексте;
по памяти: O(n + l), где l - длина текущего слова.
*/
