# SystemAnalyzer — Dark Tech Design System

**Documento:** UI-D1 — Auditoría visual y definición del sistema visual  
**Estado:** listo para revisión documental  
**Producto:** SystemAnalyzer — Asistente de Diagnóstico y Mantenimiento Preventivo de Windows  
**Autor del proyecto:** Ing. Rodrigo Flores — Carnet F1631012025  
**Tecnología auditada:** C# · Windows Forms · .NET 8 · PerMonitorV2

## 1. Alcance y fuentes

Este documento define la dirección **100% Dark Tech Dashboard** y registra el estado visual existente. UI-D1 es exclusivamente documental: no implementa componentes ni cambia navegación, eventos, servicios, modelos, fórmulas o frecuencia de muestreo.

La auditoría se hizo por lectura de:

- `Forms/FormPrincipal.cs`, `FormInicio.cs`, `FormSistema.cs`, `FormProcesos.cs`, `FormConsumo.cs`.
- `Forms/FormProcessDetails.cs`, `BaseContentForm.cs`, `FormAlmacenamiento.cs`, `FormDiagnostico.cs` y `FormCreditos.cs`.
- `UI/AppTheme.cs`, `RoundedPanel.cs`, `UsageBar.cs`, `ResponsiveLayout.cs` y `BufferedDataGridView.cs`.
- `Program.cs` y `SystemAnalyzer.csproj` para arranque, framework y configuración DPI.

Las afirmaciones marcadas **Verificado en código** describen propiedades observables en las fuentes. Las marcadas **Validación visual pendiente** requieren ejecutar desde Visual Studio en un entorno Windows con las escalas indicadas; no se infiere su resultado a partir del código.

## 2. Auditoría del estado actual

### 2.1 Paleta actual — verificado en código

| Uso actual | Valor | Localización/observación |
|---|---:|---|
| Fondo | `#0F172A` | `AppTheme.Background` |
| Barra lateral | `#111827` | `AppTheme.Sidebar` |
| Superficie | `#1E293B` | `AppTheme.Surface` |
| Hover, borde y pista de barra | `#334155` | `SurfaceHover`, `RoundedPanel`, `UsageBar` |
| Primario celeste | `#38BDF8` | `AppTheme.Primary` |
| Texto principal | `#F1F5F9` | `AppTheme.Text` |
| Texto secundario | `#94A3B8` | `AppTheme.MutedText` |
| Superficie interna/cabecera de tabla | `#263346` | Controles, unidades y rankings |
| Fila alterna | `#192435` | Tabla de procesos |
| Selección de tabla | `#1E5A78` | Tabla de procesos |
| Texto sobre botón primario | `#082F49` | Botones principales |
| CPU/RAM secundaria existente | `#A78BFA` | Ranking de RAM en `FormConsumo` |
| Favorable existente | `#34D399` | Estado general en `FormInicio` |
| Advertencia existente | `#FBBF24` | Umbrales visuales desde 75 % |
| Alerta existente | `#F87171` | Umbrales visuales desde 90 % y consumo alto |

La paleta ya es oscura y cercana a la identidad aprobada, pero combina tokens centralizados con literales repetidos. Los colores rojo y amarillo se usan para consumo, aunque consumo elevado no equivale por sí mismo a error o advertencia diagnóstica.

### 2.2 Tipografía y jerarquía — verificado en código

- Familia base: `Segoe UI`; pesos destacados: `Segoe UI Semibold` con `FontStyle.Bold`.
- Texto general: 9 pt; subtítulos: 9–10 pt; navegación: 10 pt.
- Título de página en módulos implementados: 20 pt. `BaseContentForm` usa 22 pt y el diálogo de detalle 19 pt.
- Títulos de sección/tarjeta: 10–13 pt; marca lateral y métricas: 15 pt; estado destacado: 20 pt.
- Existe una jerarquía reconocible, pero no una escala única: conviven 19, 20 y 22 pt para títulos equivalentes y 9, 9.5 y 10 pt para texto secundario.

### 2.3 Navegación lateral y encabezado — verificado en código

- `FormPrincipal` fija un mínimo de `900 × 600` y un tamaño inicial de `1340 × 730`.
- La barra lateral mide 240 px expandida y 76 px compacta; cambia automáticamente bajo 1180 px y permite alternancia manual.
- La marca ocupa 104 px de alto. Cada destino mide 54 px; el control inferior de contracción mide 48 px.
- Se usan glifos tipográficos (`⌂`, `▣`, `≡`, `◆`, `▰`, `✓`, `i`), no una familia de iconos normalizada.
- Hover y elemento activo comparten `#334155`; el estado activo no tiene marcador adicional ni diferencia semántica aparte del fondo.
- Los botones de navegación tienen `TabStop = false`.
- El encabezado global mide 72 px, usa la superficie actual y muestra sección a la izquierda y descripción a la derecha; esta última se oculta cuando el encabezado baja de 650 px.

### 2.4 Botones y estados — verificado en código

- Los botones primarios habituales miden 36 px de alto, usan fondo celeste, texto azul muy oscuro y no tienen borde.
- El estado ocupado deshabilita el botón y cambia textos como “Analizando...” o “Actualizando...”.
- No existe una implementación compartida de hover, pressed, focus visible o botón secundario.
- La configuración de botón primario está duplicada en varios formularios y algunos botones tienen `TabStop = false`; el diálogo usa una variante propia.

### 2.5 Tarjetas, paneles e indicadores — verificado en código

- `RoundedPanel` centraliza fondo, borde de 1 px, doble búfer y radio predeterminado de 14 px.
- Las tarjetas de resumen suelen medir 96–144 px; las tarjetas de contenido alcanzan 150, 210 o 426 px según el módulo.
- El padding interno efectivo suele ser 16–22 px, pero gran parte se expresa con `Location` y `Size` por control.
- `UsageBar` centraliza una barra de 7 px, limita valores a 0–100 y permite color configurable. No incorpora etiqueta, estado indeterminado ni señal no cromática.
- CPU usa el primario; RAM usa violeta en `FormConsumo`, pero otros indicadores adoptan colores por umbrales. Los datos no disponibles se muestran como texto y barra vacía.
- `FormInicio` cambia el estado general entre celeste, verde, amarillo y rojo, acompañado por texto. Sus mensajes prudentes evitan afirmar un diagnóstico permanente a partir de una muestra.

### 2.6 Tabla de procesos — verificado en código

- `BufferedDataGridView` reduce parpadeo con doble búfer.
- La tabla es de solo lectura, selección de fila completa y única, sin encabezados de fila; cabecera de 38 px y filas de 32 px.
- Columnas: nombre flexible, PID, memoria, CPU, estado y consumo; mínimo de columna 65 px.
- Ordenamiento programático con glifo, búsqueda por nombre/PID, filtro de consumo, tooltips y doble clic para detalles.
- Usa filas alternas, selección azul y scroll nativo. Los nombres se benefician de la columna `Fill`, pero el comportamiento con nombres extremos requiere prueba visual.
- Filas de consumo alto/moderado cambian todo el texto a rojo/amarillo; el significado depende demasiado del color y puede confundirse con error/advertencia.
- La barra de herramientas mantiene columnas absolutas de 190 y 118 px, sin reflow propio documentado para anchos estrechos.

### 2.7 Espaciado, responsive y DPI — verificado en código

- Las páginas implementadas usan padding exterior principalmente `22,14,22,22`; otras pantallas base usan 38 px o 24 px bajo 700 px.
- Separaciones entre tarjetas suelen ser 6–10 px; padding interno habitual 16–22 px. No existe aún una escala declarada.
- `ResponsiveLayout` redistribuye controles en `TableLayoutPanel`, calcula filas y alturas, y usa columnas porcentuales.
- Breakpoints reales: Inicio 1020/560, 760 y 850/600 px; Sistema 760, 720, 600 y 400 px; Procesos 980/500 y 820 px; Consumo 1040, 900 y 850 px.
- Inicio pasa métricas 4/2/1 y detalles 2/1. Sistema pasa tarjetas 2/1 y RAM 4/2/1. Procesos pasa resúmenes 4/2/1. Consumo pasa rankings 2/1.
- Inicio, Sistema, Consumo y formularios base contemplan desplazamiento vertical; Procesos dedica espacio flexible a la tabla.
- `AutoScaleMode.Dpi` está presente en formularios principales y el proyecto declara `PerMonitorV2`.
- Aunque se usan layouts fluidos, subsisten posiciones y tamaños absolutos internos. No deben multiplicarse como solución general.

### 2.8 Rendimiento — verificado en código

- `RoundedPanel`, `UsageBar` y `BufferedDataGridView` usan doble búfer.
- No hay animaciones visuales. Las actualizaciones asíncronas ya existentes se ejecutan cada 15 s en Inicio y cada 5 s en Procesos/Consumo.
- El rediseño no debe cambiar esas frecuencias, introducir consultas adicionales, reconstruir controles en cada pintura ni representar historial inexistente.

### 2.9 Validación visual pendiente

Debe verificarse en Visual Studio, sin asumir resultados:

- Contraste percibido, legibilidad y uniformidad de bordes en monitor real.
- Recorte, elipsis y superposición a 100 %, 125 % y 150 % DPI.
- Desplazamiento y foco visible mediante teclado en todos los módulos y el diálogo.
- Barra lateral compacta, tooltips y glifos con fuentes/configuraciones regionales distintas.
- Tabla con nombres y rutas extensas, scrollbar visible y ventana mínima.
- Alturas de textos explicativos multilínea y tarjetas al cambiar de columna.
- Parpadeo durante actualización, resize y navegación repetida.

## 3. Identidad definitiva Dark Tech Dashboard

### 3.1 Tokens de color

| Token | Hex | Uso y justificación |
|---|---:|---|
| `BackgroundPrimary` | `#0B1220` | Fondo principal aprobado; azul casi negro que conserva identidad técnica. |
| `BackgroundSecondary` | `#0F1929` | Fondo de zonas contenidas o scroll; separa planos sin aclarar la pantalla. |
| `SidebarBackground` | `#0D1626` | Navegación; ligeramente distinguible del lienzo. |
| `SurfacePrimary` | `#172438` | Tarjetas y paneles, aprobado. |
| `SurfaceSecondary` | `#1D2D44` | Controles, filas alternas y bloques anidados. |
| `SurfaceElevated` | `#22344D` | Menús, diálogos o selección elevada; usar con moderación. |
| `BorderSubtle` | `#2A3D55` | Bordes y separadores de bajo énfasis. |
| `BorderStrong` | `#3A536E` | Foco, selección o separación que necesita mayor definición. |
| `TextPrimary` | `#F2F7FC` | Titulares, valores y contenido principal. |
| `TextSecondary` | `#A9B8CA` | Descripciones y metadatos con contraste más claro que el actual. |
| `TextDisabled` | `#718198` | Contenido inactivo; siempre acompañado por estado/forma. |
| `TextOnAccent` | `#061A22` | Texto sobre acento cian para contraste. |
| `AccentPrimary` | `#27D9E8` | Acción principal, foco e indicadores CPU, aprobado. |
| `AccentPrimaryHover` | `#52E5F0` | Hover del primario; cambio visible sin brillo excesivo. |
| `AccentPrimaryPressed` | `#18B8C7` | Pressed; confirma activación por oscurecimiento. |
| `AccentSecondary` | `#A38BFF` | RAM y acento secundario, aprobado. |
| `AccentSecondaryHover` | `#B6A4FF` | Hover cuando el secundario sea interactivo. |
| `SelectionBackground` | `#164A63` | Fila o navegación seleccionada; no confundir con fondo de hover. |
| `HoverBackground` | `#213750` | Hover de navegación, tabla y controles neutros. |
| `FocusRing` | `#72E9F2` | Contorno de teclado de 2 px con alto contraste. |
| `StatusSuccess` | `#50D9AC` | Operación completada o condición favorable sustentada, aprobado. |
| `StatusWarning` | `#F2C14E` | Atención basada en regla explícita; no equivale a diagnóstico. |
| `StatusError` | `#FF707A` | Fallo confirmado o dato que no pudo recuperarse. |
| `StatusInfo` | `#62A8FF` | Información neutral y estados de actualización. |
| `Track` | `#2A3D55` | Pista de barras e indicadores. |
| `Overlay` | `#08101CCC` | Fondo modal translúcido si llega a ser necesario. |

Regla semántica: **los colores de estado describen evidencia, no sospechas**. Un porcentaje alto se etiqueta como “uso elevado en esta muestra” y conserva valor numérico/icono/texto. Rojo se reserva para error confirmado o umbral explícitamente explicado; nunca para concluir que un proceso es dañino o innecesario.

## 4. Tipografía

Fuente primaria: `Segoe UI Variable` cuando esté disponible; fallback: `Segoe UI`, después la fuente de sistema de Windows. No agregar fuentes ni paquetes externos. Para WinForms, conservar `Segoe UI` hasta validar disponibilidad y métricas de `Segoe UI Variable`.

| Rol | Tamaño | Peso | Uso |
|---|---:|---|---|
| Título principal | 20 pt | Semibold/Bold | Un título por página. |
| Título de sección | 13 pt | Semibold | Bloques principales y rankings. |
| Título de tarjeta | 10 pt | Semibold | Nombre breve de métrica. |
| Métrica destacada | 16 pt | Semibold/Bold | Valor principal; 20 pt solo para estado central. |
| Texto normal | 10 pt | Regular | Contenido y controles. |
| Texto secundario | 9 pt | Regular | Explicación breve y timestamp. |
| Etiqueta/metadato | 9 pt | Semibold/Regular | PID, unidad, ayuda y cabecera de tabla. |

Tamaño mínimo: 9 pt. Usar `AutoEllipsis` solo para contenido recuperable mediante tooltip o vista de detalle; las explicaciones deben envolver líneas y crecer verticalmente.

## 5. Espaciado y geometría

Escala base: `4, 8, 12, 16, 24, 32, 40, 48` px.

- Margen de página: 24 px; en ventanas estrechas, 16 px.
- Separación de secciones: 24 px; entre tarjetas: 12 px; entre etiqueta y valor: 8 px.
- Padding de tarjeta: 20 px (16 px en compacto).
- Altura mínima interactiva: 40 px; navegación: 52 px.
- Radio de tarjeta: 12 px; controles: 8 px; barras: radio visual máximo de 4 px si la implementación lo permite.
- Borde normal: 1 px; foco de teclado: 2 px, exterior o interior sin alterar layout.
- Las alturas son mínimas, no rígidas, cuando existe texto dinámico o multilínea.

## 6. Sistema de componentes

### A. Navegación lateral

- 240 px expandida y 72 px compacta como objetivo; transición instantánea, sin animación necesaria.
- Iconos de 20–24 px de una única familia disponible en Windows; no mezclar símbolos decorativos. Todo icono lleva etiqueta o tooltip.
- Elemento activo: fondo `SelectionBackground`, marcador izquierdo de 3 px `AccentPrimary`, texto principal y `aria` no aplica en WinForms, pero sí estado accesible/nombre descriptivo.
- Hover: `HoverBackground`; pressed: `SurfaceElevated`; foco: `FocusRing`. Ninguno reemplaza al estado activo.
- Mantener contraste AA como objetivo (4.5:1 para texto normal, 3:1 para texto grande y límites esenciales).
- Bajo el breakpoint de contenido acordado, compactar; en el mínimo de 900 px nunca superponer el contenido. No convertirla en overlay durante UI-D2 sin validar navegación por teclado.

### B. Encabezado

- Altura base 72 px, padding horizontal 24 px y borde inferior `BorderSubtle`.
- Título de sección a la izquierda; descriptor o indicador de actualización a la derecha. Ocultar primero texto auxiliar, no estado crítico.
- Evitar duplicar título global y título de página con el mismo énfasis: el encabezado global orienta; el título de página estructura contenido.

### C. Tarjetas

- Fondo `SurfacePrimary`, borde 1 px `BorderSubtle`, radio 12 px, padding 20 px y separación 12 px.
- Orden interno: etiqueta → valor → contexto/unidad → indicador opcional.
- Sin sombras pesadas ni glow. `AccentPrimary` se usa en datos o acciones, no como borde universal.
- Altura flexible y `MinimumSize`; las explicaciones multilínea crecen. Una tarjeta no debe cortar texto para mantener simetría.

### D. Botones

- Primario: `AccentPrimary` + `TextOnAccent`; hover/pressed con tokens correspondientes.
- Secundario: `SurfaceSecondary`, texto principal y borde `BorderStrong`; hover `HoverBackground`.
- Deshabilitado: `SurfaceSecondary` + `TextDisabled`, cursor normal; conservar etiqueta legible.
- Cargando: deshabilitado, texto de acción en progreso y, solo si resulta estable, indicador estático o spinner liviano. No bloquear el hilo UI.
- Foco: contorno de 2 px `FocusRing`. Los controles accionables deben participar en el orden de tabulación; no usar `TabStop = false` salvo justificación explícita.
- Altura mínima 40 px y padding horizontal 16 px.

### E. Indicadores

- CPU: `AccentPrimary`; RAM: `AccentSecondary`; pista `Track`; valor numérico siempre visible.
- Uso elevado se expresa con valor + texto (“elevado en esta muestra”) y no solo color. Los umbrales deben provenir de reglas existentes y explicarse.
- Desconocido: “No disponible”, pista vacía con patrón/ícono neutral si se implementa; nunca mostrar 0 % como sustituto.
- Actualizando: conservar último valor cuando sea válido y marcar “Actualizando”; si no existe valor, “Calculando”. No animar continuamente.

### F. Tablas

- Cabecera 40 px, fondo `SurfaceSecondary`, texto semibold; filas de al menos 36 px.
- Alternancia sutil entre `SurfacePrimary` y `BackgroundSecondary`; separadores `BorderSubtle`.
- Selección `SelectionBackground` con texto principal y foco visible. Estado semántico mediante texto/icono además de color.
- Scroll nativo oscuro cuando el sistema lo permita sin paquetes; no ocultar barras necesarias.
- Ordenamiento conserva glifo ascendente/descendente y nombre accesible. Nombre de proceso ocupa columna flexible, usa elipsis y tooltip; ruta completa en detalle.
- En ancho mínimo, priorizar Nombre, CPU y RAM; las columnas secundarias pueden reducirse u ocultarse solo si siguen accesibles en detalle.

### G. Mensajes

- Informativo: `StatusInfo` + icono “i” + título breve.
- Advertencia: `StatusWarning` + icono/etiqueta “Atención”; solo ante evidencia o limitación concreta.
- Error recuperable: `StatusError`, explicación y acción “Reintentar”; no culpabilizar al usuario.
- Vacío: mensaje neutral, causa posible y siguiente acción; no usar rojo.
- No disponible: mostrar literalmente “No disponible” y, cuando ayude, el motivo. No inventar cero ni mediciones.

## 7. Responsive y DPI: criterios verificables

- Modos objetivo: maximizada, normal y mínimo `900 × 600`. No reducir el mínimo sin una auditoría funcional de tabla y navegación.
- El ancho útil, no el ancho total de pantalla, gobierna breakpoints. Conservar el patrón `TableLayoutPanel`/`FlowLayoutPanel` y evitar posiciones absolutas como estrategia general.
- Tarjetas: dos columnas cuando cada tarjeta conserve al menos 280 px; una columna debajo de ese límite. Cuatro columnas solo si cada tarjeta conserva contenido y padding.
- Habilitar desplazamiento vertical de página cuando el contenido exceda el alto; evitar scrolls verticales anidados salvo la tabla.
- Textos explicativos usan wrap y altura automática/mínima. Nombres largos usan elipsis + tooltip + detalle.
- A 100 %, 125 % y 150 %: ningún texto se recorta; controles no se superponen; foco y botones permanecen completos; tarjetas conservan padding; diálogo cabe en área de trabajo.
- Probar cambio de monitor con `PerMonitorV2`, restaurar/maximizar y resize continuo. Revisar especialmente controles con `Location`, `Size` o columnas absolutas.

Matriz manual mínima:

| Escenario | Verificación |
|---|---|
| Maximizada, 100 % | Jerarquía, densidad, cuatro/dos columnas y tabla. |
| Normal, 100 % | Breakpoints, sidebar expandida/compacta y scroll. |
| `900 × 600`, 100 % | Sin solapamientos; acciones, tabla y navegación utilizables. |
| Normal, 125 % | Texto, botones, cabeceras, tarjetas y tooltips sin corte. |
| Normal y maximizada, 150 % | Reflow, scroll, diálogo y cambio PerMonitorV2. |
| Datos extremos | Nombres/rutas largos, valores desconocidos, lista vacía y error recuperable. |

## 8. Rendimiento y accesibilidad

- Mantener doble búfer y actualizar solo controles que cambian; evitar recreación visual innecesaria y animaciones decorativas.
- No agregar consultas, no variar intervalos de 15 s/5 s y no bloquear UI. El diseño presenta datos existentes; no simula series históricas.
- Objetivo de contraste WCAG AA. Validar combinaciones finales con herramienta de contraste antes de UI-D5.
- Todo estado usa al menos dos señales: texto/valor/icono/forma además de color.
- Orden Tab lógico: sidebar → encabezado/acciones → filtros → contenido → tabla. `Enter`/`Space` activan botones y el foco siempre es visible.
- Respetar configuración de Windows y no depender de hover. Tooltips complementan, nunca contienen la única información esencial.

## 9. Componentes reutilizables y evolución

| Componente real | Reutilización prevista | Ajuste futuro, no incluido en UI-D1 |
|---|---|---|
| `AppTheme` | Fuente única de tokens | Incorporar paleta definitiva y factorías de estilos. |
| `RoundedPanel` | Base de tarjetas | Tokens de radio/borde y layout interno consistente. |
| `UsageBar` | CPU, RAM y almacenamiento | Estado desconocido, semántica y señal no cromática. |
| `ResponsiveLayout` | Reflow por columnas/contenido | Unificar breakpoints y márgenes de la escala. |
| `BufferedDataGridView` | Tabla de procesos | Estilo compartido, accesibilidad y reducción de parpadeo. |
| `BaseContentForm` | Pantallas futuras/provisionales | Alinear jerarquía, padding y scroll con páginas implementadas. |
| `SummaryCard`/`MetricCard` locales | Patrón probado de métricas | Extraer un componente compartido para evitar duplicación. |
| Configuración local de botón | Patrón funcional | Centralizar variantes y estados de interacción. |

## 10. Inconsistencias y deuda visual priorizada

1. Paleta aprobada y paleta actual no coinciden exactamente; varios colores viven como literales.
2. Hover y selección activa de navegación son iguales; falta foco visible y marcador activo.
3. Botones primarios, tarjetas de resumen y helpers tipográficos están duplicados.
4. Varios botones accionables excluyen navegación Tab.
5. El significado de consumo alto/moderado depende en exceso de rojo/amarillo y puede aparentar diagnóstico.
6. Escala tipográfica y espaciado no están unificados; existen títulos equivalentes de 19/20/22 pt.
7. Breakpoints varían por formulario sin una regla transversal.
8. Controles internos y toolbar de Procesos conservan medidas/columnas absolutas que requieren prueba en 125/150 %.
9. `UsageBar` no distingue visualmente “0” de “desconocido” por sí sola.
10. Iconografía de sidebar usa glifos heterogéneos dependientes de fuente.
11. Estados hover/pressed/focus/disabled no están completos ni centralizados.
12. `BaseContentForm` y páginas de dashboard usan estructuras visuales diferentes.

## 11. Plan de implementación

### UI-D1 — Auditoría y documentación del sistema visual

- **Alcance:** inventario real, tokens, reglas de componentes, accesibilidad, responsive, DPI y plan.
- **Archivo afectado:** `DESIGN_SYSTEM.md` únicamente.
- **Riesgos:** conclusiones visuales sin ejecutar la aplicación; mitigación: etiquetar validaciones pendientes.
- **Aceptación:** documento coherente con clases reales, paleta completa, reglas verificables y ninguna modificación funcional.
- **Pruebas manuales:** revisión documental y contraste con código; no requiere compilación.

### UI-D2 — Barra lateral, encabezado y componentes compartidos

- **Alcance:** tokens en `AppTheme`, navegación/encabezado, estilos compartidos de botón, tarjeta, indicador y foco.
- **Archivos potenciales:** `UI/AppTheme.cs`, `RoundedPanel.cs`, `UsageBar.cs`, posibles nuevos componentes en `UI/`, `Forms/FormPrincipal.cs`, `BaseContentForm.cs`.
- **Riesgos:** regresión de navegación, orden Tab, clipping DPI, diferencias entre controles nativos.
- **Aceptación:** identidad nueva aplicada a shell/componentes, estados completos, sin cambios de servicios/eventos funcionales.
- **Pruebas manuales:** navegar con mouse/teclado; 900×600, normal/maximizada; DPI 100/125/150; cambio de monitor.

### UI-D3 — Dashboard Inicio

- **Alcance:** aplicar tarjetas, métricas, jerarquía y estados al dashboard sin alterar datos ni muestreo.
- **Archivos potenciales:** `Forms/FormInicio.cs` y componentes compartidos ya creados en UI-D2.
- **Riesgos:** textos dinámicos recortados, confundir umbral con diagnóstico, reflow y scroll.
- **Aceptación:** CPU/RAM/almacenamiento/procesos legibles; desconocido y actualización inequívocos; 4/2/1 columnas estables.
- **Pruebas manuales:** carga, actualización, error recuperable, datos no disponibles, tamaños/DPI objetivo y resize repetido.

### UI-D4 — Sistema, Procesos y Mayor consumo

- **Alcance:** aplicar sistema visual a `FormSistema`, `FormProcesos`, `FormConsumo` y `FormProcessDetails`; mantener fórmulas, filtros y ordenamiento.
- **Archivos potenciales:** esos formularios y componentes compartidos; `Models` y `Services` quedan fuera salvo una tarea funcional separada.
- **Riesgos:** tabla densa, columnas/rutas largas, semántica de consumo, rankings en una columna y diálogo a DPI alto.
- **Aceptación:** filtros/orden/detalle intactos; estados no dependen solo de color; tablas y rankings legibles en mínimo y DPI alto.
- **Pruebas manuales:** búsqueda/PID, filtros, cada orden, selección/doble clic, proceso finalizado, nombres largos, CPU desconocida, scroll y ranking 2/1.

### UI-D5 — Integración, responsive, DPI y validación visual

- **Alcance:** integrar pantallas, resolver inconsistencias restantes y ejecutar la matriz visual completa.
- **Archivos potenciales:** formularios y UI previamente intervenidos; `.csproj` solo si una necesidad técnica se aprueba por separado.
- **Riesgos:** correcciones locales que rompan otros breakpoints, contraste insuficiente, flicker o regresión funcional.
- **Aceptación:** cero solapamientos/cortes en matriz; contraste y teclado aprobados; coherencia visual total; rendimiento y frecuencia sin cambios.
- **Pruebas manuales:** recorrido completo en Visual Studio, 100/125/150 %, mínimo/normal/maximizada, cambio PerMonitorV2, estados extremos, actualización y navegación prolongada.

## 12. Criterio de cierre de UI-D1

UI-D1 queda lista para revisión cuando este sea el único archivo modificado por la tarea, el contenido corresponda al código real, Git se haya consultado solo en lectura y no exista ningún cambio en archivos C#, recursos o proyecto. La implementación comienza únicamente con autorización explícita para UI-D2.
