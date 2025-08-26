
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

2. **Crea una rama** desde `main` actualizado **usando TU usuario de GitHub**:
   - **Convención obligatoria**: `<usuario>/<tipo>-<slug>`  
     - *Tipos válidos*: `feat`, `fix`, `docs`, `chore`, `refactor`, `test`, `ci`, `perf`, `style`  
     - *Ejemplos*: `kevindoblea1/feat-pantalla-lista`, `mariaqa/fix-nullref-inicializacion`

   **WSL / Git Bash:**
   ```bash
   git fetch upstream
   git checkout main
   git rebase upstream/main
   USER=$(gh api user --jq .login)
   git switch -c "$USER/feat-inicial-setup"
   ```

   **Windows PowerShell:**
   ```powershell
   git fetch upstream
   git checkout main
   git rebase upstream/main
   $USER = gh api user --jq .login
   git switch -c "$USER/feat-inicial-setup"
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
   git push -u origin $(git branch --show-current)
   # Crea el PR (detecta la rama actual automáticamente):
   gh pr create --base main --fill
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
- ✅ **Nombre de rama válido** según la convención `<usuario>/<tipo>-<slug>`

---

## Estilo de ramas (resumen)

- **Formato obligatorio**: `<usuario>/<tipo>-<slug>`
- **Tipos**: `feat`, `fix`, `docs`, `chore`, `refactor`, `test`, `ci`, `perf`, `style`
- **Ejemplos**: `kevindoblea1/feat-pantalla-lista`, `ana-dev/docs-contributing`

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
- [ ] **La rama sigue** `<usuario>/<tipo>-<slug>`
---

## Trabajar en **Visual Studio 2022** (Windows)

Estas instrucciones son para que cada contribuidor pueda **clonar, crear su rama**, compilar y **depurar** Listify en **Visual Studio 2022**.

### 0) Requisitos en Windows
- **Visual Studio 2022 17.10+**
- Cargas de trabajo (Visual Studio Installer → *Modify*):
  - ✅ **.NET Multi-platform App UI development** (MAUI)
  - ✅ **.NET Desktop Development** (opcional pero útil)
  - Componentes recomendados: **Windows 10/11 SDK (10.0.19041+)**, **Android SDK**, **Android Emulators**, **OpenJDK 17**.

> Alternativa CLI (opcional): `dotnet workload install maui`

### 1) Obtener el repositorio en local
**Opción A – Clonar desde Visual Studio**
1. Abre **Visual Studio 2022** → **Git** → **Clone Repository**.
2. En **Repository location**, pega la URL de **tu fork** (https://github.com/<tu-usuario>/Listify).
3. Elige una carpeta local y haz clic en **Clone**.

**Opción B – Ya lo clonaste con `gh`/`git`**
1. En Visual Studio: **File** → **Open** → **Project/Solution** → selecciona `Listify.sln` en tu carpeta local.

### 2) Crear tu **rama** con tu usuario (dentro de VS)
1. Abre la ventana **Git Changes** (View → Git Changes).
2. Clic en **New Branch**.
3. Nombra la rama con la convención **obligatoria**: `<usuario>/<tipo>-<slug>`  
   - Ej.: `kevindoblea1/feat-pantalla-lista`, `ana-dev/fix-crash-inicial`
   - Tipos válidos: `feat`, `fix`, `docs`, `chore`, `refactor`, `test`, `ci`, `perf`, `style`
4. Base branch: **main**. Clic en **Create**.
5. **Publica** la rama: botón **Publish Branch** (esto hace `git push -u`).
   - Si te pregunta, **establece upstream**.

> Si lo prefieres por consola: `git switch -c <usuario>/<tipo>-<slug>` → `git push -u origin $(git branch --show-current)`

### 3) Restaurar paquetes y compilar
- Visual Studio normalmente ejecuta **Restore** automáticamente.
- Manual: **Build** → **Restore NuGet Packages**.
- Compila: **Build** → **Build Solution** (Ctrl+Shift+B).

### 4) Ejecutar y depurar (Windows y Android)
- En la barra superior, selecciona el **target**:
  - **Windows**: elige **Windows Machine** (o *Local Machine*) y presiona **F5**.
  - **Android**: elige un **emulador** o **dispositivo físico** y presiona **F5**.

**Configurar Emulador Android (si no aparece ninguno):**
1. **Tools** → **Android** → **Android Device Manager**.
2. **New** → elige un dispositivo (p. ej. *Pixel 5*), **API 34** o cercana → **Create**.
3. **Start** y vuelve a la barra de targets para seleccionarlo.

### 5) Tests (si aplica)
- **Test** → **Test Explorer**.
- Ejecuta todos o por proyecto/prueba.
- Línea de comandos equivalente: `dotnet test`.

### 6) Cambios, commits y push desde VS
1. Ventana **Git Changes** → escribe mensaje de commit (usa **Conventional Commits**).
2. **Stage** (si usas staging) y **Commit**.
3. **Push**: botón **Push** para enviar tu rama a tu fork.

### 7) Crear el Pull Request desde VS
- **Git** → **Create Pull Request** (o **View → Git Repository Window** → botón **Create PR**).
- Base: `upstream/main` (repo original) ← Compare: `origin/<tu-rama>` (tu fork/tu rama).
- Rellena título y descripción (usa checklist del CONTRIBUTING).

> Alternativa CLI rápida: `gh pr create --base main --fill`

### 8) Consejos y problemas comunes
- **Java 17 / Android SDK**: si falla Android con errores de Java/AAPT, abre **Tools** → **Options** → **Xamarin/Android Settings** y verifica **JDK** (17) y rutas del SDK.
- **No aparecen emuladores**: instala desde **Visual Studio Installer** los componentes de Android Emulators y Android SDK Platforms.
- **`dotnet format`**: desde VS abre **Terminal** integrada (**View** → **Terminal**) y ejecuta `dotnet format` antes de hacer push.
- **SQLite**: si cambias esquemas, recuerda migraciones o inicialización en servicios. Verifica rutas de DB en Android/Windows.
- **Ramas**: si VS propone un nombre de rama distinto, renómbrala al formato `<usuario>/<tipo>-<slug>` para que pase el *workflow* de verificación.

Listo: con esto cada desarrollador puede clonar su fork, crear su propia rama con su **usuario**, y desarrollar/depurar Listify directamente en **Visual Studio 2022**.
