# Listify — Lista de Compras (.NET MAUI + SQLite)

[![Build Android](https://img.shields.io/github/actions/workflow/status/Grupo-Listify/Listify/build-android.yml?branch=main&label=Android%20CI)](../../actions)
[![Build Windows](https://img.shields.io/github/actions/workflow/status/Grupo-Listify/Listify/build-windows.yml?branch=main&label=Windows%20CI)](../../actions)
[![Release](https://img.shields.io/github/v/release/Grupo-Listify/Listify?display_name=tag)](../../releases)
[![License: MIT](https://img.shields.io/github/license/Grupo-Listify/Listify)](./LICENSE)

Aplicación mínima de lista de compras para Windows/Android/iOS. Permite agregar artículos, marcarlos como comprados, editarlos, eliminarlos y buscar por texto. Pila: **.NET 8, .NET MAUI, SQLite (sqlite-net-pcl)**.

## Demo / Capturas
Coloca tus capturas en `docs/img/` y enlázalas aquí.
![demo](docs/img/demo-1.png)

## Descarga (End-Users)
👉 **Última versión**: dirígete a [Releases](../../releases) para descargar el APK (Android) y el paquete Windows cuando estén disponibles.

## Desarrollo (Dev Setup)
Requisitos: Visual Studio 2022 17.9+, .NET 8 SDK y workloads de MAUI.
```bash
dotnet workload install maui
dotnet restore
dotnet build -c Debug
# Publicar Android
dotnet publish -c Release -f net8.0-android
```
> Para Windows, usa `-f net8.0-windows`. Revisa los artefactos en `bin/Release/...`.

## Características
- ✅ CRUD de artículos (SQLite)
- ✅ Marcar/Desmarcar como comprado
- ✅ Búsqueda en tiempo real
- ✅ Arquitectura MVVM

## Roadmap
- [ ] Pantalla de estadísticas
- [ ] Exportar/Importar en CSV/ZIP
- [ ] Tests de base de datos

## Contribuir
Lee [CONTRIBUTING.md](CONTRIBUTING.md) y abre un Issue/PR. Usa ramas `feature/*` o `bugfix/*`. Todas las PRs corren CI y requieren revisión.

## Licencia
MIT © 2025 Kevin Alvarenga
