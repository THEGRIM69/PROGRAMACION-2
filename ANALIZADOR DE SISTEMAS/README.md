# SystemAnalyzer

**Asistente de Diagnóstico y Mantenimiento Preventivo de Windows** desarrollado como aplicación académica de escritorio con C#, Windows Forms y .NET 8.

SystemAnalyzer recopila datos reales del equipo, ayuda a interpretar su rendimiento y ofrece una base para emitir recomendaciones preventivas seguras y explicables. Su propósito no es reemplazar al Administrador de tareas: mientras este muestra qué consume recursos, SystemAnalyzer busca explicar qué significa ese consumo, cuándo puede requerir atención y qué medidas prudentes podrían ser útiles.

El programa no promete aceleraciones automáticas, no modifica Windows y no debe presentar hipótesis como hechos confirmados.

## Estado actual

El proyecto se encuentra al cierre de la **Fase 4**.

| Fase | Estado | Alcance |
|---|---|---|
| 1 | Completada | Arquitectura, navegación y formularios base |
| 2 | Completada | Dashboard con CPU, RAM, procesos y almacenamiento |
| 3 | Completada | Información detallada del sistema |
| 3.1 | Completada | Diseño responsive |
| 3.2 | Completada | Correcciones visuales y de desplazamiento |
| 4 | Completada con deuda técnica registrada | Analizador de procesos |
| 5 a 10 | Planificadas | Nuevo enfoque preventivo |

La última versión verificada de Fase 4 corresponde al commit `bd050af`, `feat: implementar analizador de procesos fase 4`.

Las fases futuras y los criterios de aceptación de la Fase 5 están definidos en [ROADMAP.md](ROADMAP.md). Las observaciones pendientes de la auditoría están registradas en [DEUDA_TECNICA.md](DEUDA_TECNICA.md).

## Módulos

- **Inicio:** resume la salud y el rendimiento actual del equipo. Sus indicadores son una fotografía del momento y no prueban por sí solos un problema permanente.
- **Sistema:** presenta las características técnicas necesarias para contextualizar las mediciones y los diagnósticos.
- **Procesos:** permite explorar los procesos activos, su CPU, memoria, estado y nivel de consumo, sin ofrecer acciones destructivas.
- **Mayor consumo:** interpretará el impacto de programas sobre CPU y RAM, evitando duplicar la tabla general de Procesos. Su implementación corresponde a la Fase 5.
- **Almacenamiento:** evaluará preventivamente la ocupación y el espacio disponible. No afirmará la salud física de una unidad basándose solo en su ocupación.
- **Diagnóstico:** reunirá hallazgos sustentados, evidencia, impacto posible, prioridad, recomendaciones y limitaciones.
- **Créditos:** conserva la autoría y la información académica del proyecto.

## Principios del producto

- Obtener todas las mediciones de fuentes reales; nunca inventar porcentajes.
- Indicar el denominador y el período de observación cuando sean relevantes.
- No considerar peligroso un proceso únicamente por usar mucha memoria.
- No convertir una sola muestra en una conclusión permanente.
- Separar observaciones, posibles causas, evidencias y recomendaciones.
- Expresar las limitaciones y la información insuficiente.
- No recomendar finalizar procesos esenciales de Windows.
- No ejecutar modificaciones automáticas sobre el sistema operativo.
- Favorecer recomendaciones preventivas, reversibles y comprensibles.

## Tecnología

- C#
- Windows Forms
- .NET 8 para Windows (`net8.0-windows`)
- Solución: `SystemAnalyzer.sln`

## Abrir y ejecutar

1. Abra `SystemAnalyzer.sln` con Visual Studio 2022.
2. Espere a que Visual Studio restaure el proyecto.
3. Presione `F5` o el botón **Iniciar**.

También puede compilar desde una terminal con `dotnet build SystemAnalyzer.sln`.

## Autoría

**Ing. Rodrigo Flores**  
**Carnet F1631012025**
