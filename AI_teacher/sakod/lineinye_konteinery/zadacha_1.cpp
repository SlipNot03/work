#include <iostream>
#include <stack>
#include <string>

using namespace std;

bool isOpening(char c) {
    return c == '(' || c == '[' || c == '{';
}

bool isPair(char open, char close) {
    return (open == '(' && close == ')') ||
           (open == '[' && close == ']') ||
           (open == '{' && close == '}');
}

int main() {
    string s;
    cin >> s;

    stack<char> brackets;
    bool correct = true;

    for (int i = 0; i < (int)s.length(); i++) {
        char c = s[i];

        if (isOpening(c)) {
            brackets.push(c);
        } else {
            if (brackets.empty() || !isPair(brackets.top(), c)) {
                correct = false;
                break;
            }
            brackets.pop();
        }
    }

    if (!brackets.empty()) {
        correct = false;
    }

    if (correct) {
        cout << "YES";
    } else {
        cout << "NO";
    }

    return 0;
}

/*
Пример входных данных:
([]{})

Пример выходных данных:
YES

Сложность:
по времени: O(n), где n - длина строки;
по памяти: O(n), если все открывающие скобки лежат в стеке.
*/
