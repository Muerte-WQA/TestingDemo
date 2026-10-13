# TestingDemo

Учебный проект: демонстрация видов и типов тестирования на C# (.NET Framework 4.8).

## Структура

- `TestingDemo.Core` — тестируемая библиотека (Calculator, UserService, OrderProcessor)
- `tests/` — 8 проектов с разными видами тестирования
- `benchmarks/` — проект BenchmarkDotNet

## Как собрать

1. Откройте `TestingDemo.sln` в Visual Studio 2022/2026.
2. **Сборка → Собрать решение** (Ctrl+Shift+B).

## Как запустить тесты

**Тест → Запустить все тесты** (Ctrl+R, A).

## Виды тестирования

| Проект | Что демонстрирует |
|---|---|
| UnitTests | Модульные тесты |
| IntegrationTests | Интеграционные (с Moq) |
| SystemTests | Сквозные (E2E) |
| RegressionTests | Регрессионные |
| FunctionalTests | Функциональные |
| PerformanceTests | Нагрузочные |
| SecurityTests | Безопасности |
| AcceptanceTests | Приёмочные |