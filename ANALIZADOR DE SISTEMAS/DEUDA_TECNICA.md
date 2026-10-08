# Deuda técnica y observaciones de auditoría

Registro derivado de la auditoría efectuada al cierre de la Fase 4. Este documento no implica que los puntos hayan sido corregidos.

## Prioridad media

### Identidad de procesos y reutilización de PID

`ProcessAnalyzerService` compara PID, nombre y hora de inicio para relacionar muestras de CPU. Desde la Fase 5, la muestra solo se atribuye cuando ambas horas de inicio están disponibles y coinciden. Si la identidad no puede verificarse, la CPU se presenta como no disponible en lugar de asociarla por PID y nombre. La consulta de detalles todavía vuelve a localizar el proceso únicamente por PID.

**Riesgo pendiente:** entre la selección de una fila y la consulta de detalles, un PID reutilizado todavía podría mostrar una identidad diferente a la seleccionada.

**Corrección aplicada en Fase 5:** se descarta la comparación de CPU cuando no puede confirmarse la hora de inicio.

**Tratamiento pendiente:** conservar la identidad esperada y revalidar nombre y hora de inicio antes de mostrar detalles.

### Artefactos generados rastreados por Git

La auditoría encontró aproximadamente 30 archivos modificados bajo directorios `obj/` de otros proyectos del repositorio. No pertenecen a SystemAnalyzer y no deben limpiarse ni restaurarse desde este proyecto.

**Riesgo:** ruido en el estado de Git y posibilidad de incluir artefactos ajenos en un commit manual.

**Tratamiento recomendado:** revisar por separado la política de seguimiento e ignorado del repositorio, preservando primero cualquier cambio legítimo. Esta tarea queda fuera del alcance de SystemAnalyzer.

## Prioridad baja

### Liberación explícita de ToolTip

Algunos formularios crean instancias de `ToolTip` directamente y no documentan su liberación explícita.

**Riesgo:** retención de recursos del componente durante ciclos repetidos de creación y destrucción de formularios.

**Tratamiento recomendado:** incorporar los componentes a un contenedor desechable o liberarlos de forma idempotente junto con los demás recursos del formulario.

### Estado simplificado de los procesos

El modelo presenta el estado predeterminado `Ejecutándose`; no diferencia procesos sin respuesta, suspendidos u otros estados.

**Riesgo:** el usuario podría interpretar la columna como una evaluación más detallada de la que realmente existe.

**Tratamiento recomendado:** renombrar o explicar el alcance del campo, o implementar estados verificables sin inferencias inseguras.

### Documentación histórica desactualizada

El README anterior declaraba solamente la Fase 1 como completada.

**Estado documental:** actualizado durante la transición oficial al nuevo enfoque. Se conserva este punto como antecedente de auditoría.

## Restricciones para resolver la deuda

- No alterar la exactitud de las mediciones existentes sin pruebas comparativas.
- No introducir funciones para terminar procesos ni modificar Windows.
- No mezclar la limpieza de artefactos de otros proyectos con cambios de SystemAnalyzer.
- Resolver los puntos mediante cambios pequeños, revisables y acompañados de criterios de prueba.
