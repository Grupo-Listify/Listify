# Contribuir a **Listify**

¡Gracias por tu interés en contribuir! 🎉  
Listify es una app **.NET MAUI** (arquitectura **MVVM**) con persistencia en **SQLite** sobre **.NET 8**.

> Si es tu primera contribución, abre un *issue* para discutir la propuesta antes de abrir el PR.

---

## Requisitos

- **Windows 11 / macOS / Linux**
- **.NET SDK 8.0** (`dotnet --version`)
- **Workload MAUI** (si vas a compilar móviles): `dotnet workload install maui`
- **Visual Studio 2022 17.10+** (opcional) con el paquete **.NET MAUI**
- **Android SDK** y **Java 17** si corres en Android

---

## Flujo de trabajo (fork → rama → PR)

1. **Haz fork** del repo y **clónalo**:
   ```bash
   gh repo fork kevindoblea1/Listify --clone=true --remote=true
   cd Listify
   ```
   > Con `gh` tendrás `origin` → tu fork y `upstream` → repo original.

2. **Crea una rama** desde `main` actualizado:
   ```bash
   git fetch upstream
   git checkout main
   git rebase upstream/main
   git switch -c feat/<breve-descripcion>
   ```

3. **Desarrollo local**
   - Restaurar paquetes: `dotnet restore`
   - Compilar: `dotnet build`
   - Ejecutar (Windows): `dotnet run`
   - MAUI Android: `dotnet build -t:Run -f net8.0-android`
   - MAUI Windows: `dotnet build -t:Run -f net8.0-windows10.0.19041.0`

4. **Commits**
   - Usa **Conventional Commits**:
     - `feat:`, `fix:`, `docs:`, `chore:`, `refactor:`, `test:`, `ci:`, `perf:`, `style:`
   - Ejemplos:
     - `feat(items): agrega marcado como comprado`
     - `fix(db): corrige inicialización de sqlite en Windows`
     - `docs(readme): instrucciones de instalación`

5. **Push & PR**
   ```bash
   git push -u origin feat/<breve-descripcion>
   gh pr create --base main --head <tu-usuario>:feat/<breve-descripcion> --title "feat: ..." --fill
   ```

---

## Estándares de código

- **C#** con estilo consistente:
  - Ejecuta `dotnet format` antes de subir cambios.
  - Evita *warnings* nuevos; idealmente compila **sin** warnings.
- **MVVM**:
  - Lógica en **ViewModels**, nada de lógica en code-behind de Views.
  - **INotifyPropertyChanged** / **CommunityToolkit.Mvvm** para *bindings*.
- **Data**:
  - Acceso a SQLite en **Services/Repositories**.
  - Evita acoplar la vista con acceso a datos.

---

## Tests

- Si existe la carpeta `tests/`, agrega/actualiza pruebas (xUnit/NUnit/MSTest).
- Comando recomendado:
  ```bash
  dotnet test --nologo --verbosity minimal
  ```
- Para bugs: incluye al menos **1 test** que falle sin tu fix y pase con tu fix.

---

## Estructura (referencial)

- `Listify.sln` / `Listify.csproj`
- `App.xaml`, `MauiProgram.cs`, `Views/`, `ViewModels/`, `Models/`, `Services/`, `Data/`
- `Resources/` (estilos, imágenes), `Properties/`
- `README.md`, `LICENSE`, `CONTRIBUTING.md`

> Si agregas carpetas nuevas, documenta en el README brevemente su propósito.

---

## Issues: cómo reportar

Antes de crear un Issue:
1. Busca si ya existe algo similar.
2. Proporciona:
   - **Descripción breve** y **pasos para reproducir**
   - **Resultado esperado** vs **resultado actual**
   - **Logs / mensajes** relevantes
   - **Entorno** (SO, versión .NET SDK, plataforma destino)

**Labels sugeridas**: `bug`, `feature`, `documentation`, `good first issue`, `help wanted`.

---

## Criterios de aceptación (PR)

Tu PR será revisado si cumple:

- ✅ Título y commits con **Conventional Commits**
- ✅ Compila y corre en al menos **una plataforma** (Windows/Android)
- ✅ Sin *warnings* nuevos; `dotnet format` aplicado
- ✅ Incluye **tests** si aplica (nuevo feature o bug fix)
- ✅ Actualiza **README**/**docs** si agregas funcionalidad visible
- ✅ Descripción con **qué** cambia, **por qué** y **cómo probarlo**
- ✅ **Screenshots**/GIF si afecta UI

---

## Estilo de ramas

- `feat/<algo>` nuevas funciones
- `fix/<algo>` correcciones de bug
- `docs/<algo>` documentación
- `chore/<algo>` mantenimiento (build, tooling, CI)
- `refactor/<algo>` cambios internos sin alterar funcionalidad

---

## Seguridad

Si encuentras una vulnerabilidad, **no** abras un issue público.  
Escribe por privado al mantenedor (o usa *Security Advisories* si están habilitados).

---

## Licencia

Al contribuir aceptas que tu aporte se licencia bajo **MIT** del proyecto.

---

## Checklist para PR (copia/pega en la descripción)

- [ ] Título/commits cumplen **Conventional Commits**
- [ ] `dotnet restore && dotnet build` sin errores
- [ ] `dotnet format` aplicado
- [ ] Tests agregados/actualizados (si aplica) y `dotnet test` OK
- [ ] Docs/README actualizados (si aplica)
- [ ] Screenshots/GIF incluidos (si UI)
