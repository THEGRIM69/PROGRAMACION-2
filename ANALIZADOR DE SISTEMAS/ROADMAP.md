# Roadmap oficial de SystemAnalyzer

## Dirección del producto

SystemAnalyzer evoluciona de un visor de rendimiento a un **Asistente de Diagnóstico y Mantenimiento Preventivo de Windows**. Las fases 1 a 4 se conservan como base funcional. Las fases siguientes deben transformar mediciones reales en explicaciones y recomendaciones prudentes, sin atribuir causalidad cuando la evidencia no sea suficiente.

## Fases completadas

- **Fase 1:** arquitectura, navegación y formularios.
- **Fase 2:** Dashboard con CPU, RAM, procesos y almacenamiento.
- **Fase 3:** información detallada del sistema.
- **Fase 3.1:** diseño responsive.
- **Fase 3.2:** correcciones visuales y desplazamiento.
- **Fase 4:** analizador de procesos.

## Fase 5 — Análisis de impacto de programas

Convertir **Mayor consumo** en una herramienta de interpretación del impacto de procesos y aplicaciones.

Alcance:

- Identificar los mayores consumidores de CPU y RAM.
- Expresar cuánto representa cada consumo respecto al equipo mediante denominadores explícitos.
- Distinguir una lectura puntual de evidencia sostenida.
- Presentar observaciones comprensibles y limitadas por la evidencia disponible.
- Reutilizar los servicios y las muestras existentes, sin duplicar innecesariamente las consultas del módulo Procesos.
- Mantener el módulo como herramienta de análisis, sin acciones para terminar procesos o modificar Windows.

### Criterios de aceptación verificables

#### Fuentes y exactitud

- [ ] El ranking utiliza datos reales obtenidos de los servicios del proyecto; no contiene valores simulados ni porcentajes ficticios.
- [ ] Se ofrecen rankings separados o claramente diferenciados para CPU y RAM.
- [ ] El orden del ranking se realiza con valores numéricos, no con los textos formateados de la interfaz.
- [ ] Cada porcentaje identifica su denominador. Para RAM de proceso se usa memoria física total; para CPU se documenta la normalización aplicada y el intervalo de muestreo.
- [ ] Los valores no disponibles se muestran como tales y no se sustituyen por cero cuando cero alteraría la interpretación.

#### Interpretación

- [ ] Cada elemento relevante incluye una observación en lenguaje comprensible y la evidencia numérica que la sustenta.
- [ ] Un consumo alto se presenta como señal para investigar, no como prueba automática de peligro, malware o mal funcionamiento.
- [ ] La pantalla diferencia explícitamente una medición puntual de una tendencia sostenida.
- [ ] Las conclusiones indican sus limitaciones, especialmente durante la primera muestra de CPU o cuando faltan permisos.
- [ ] No se atribuye lentitud general a un solo proceso sin evidencia suficiente.

#### Procesos protegidos y datos incompletos

- [ ] Los procesos protegidos o finalizados durante la consulta no bloquean la actualización completa.
- [ ] Los campos inaccesibles se presentan como `No disponible` o una explicación equivalente.
- [ ] La ausencia de datos no provoca que un proceso sea clasificado automáticamente como seguro, peligroso o de bajo impacto.

#### Integración y eficiencia

- [ ] La implementación reutiliza `ProcessAnalyzerService`, `ProcessInfo`, `SystemInfoService` y las reglas comunes que resulten aplicables.
- [ ] No inicia consultas duplicadas cuando ya existe una muestra vigente que puede compartirse o reutilizarse.
- [ ] Impide ciclos de actualización simultáneos.
- [ ] La recopilación se ejecuta de forma asíncrona y la interfaz permanece interactiva.
- [ ] Las actualizaciones pueden cancelarse al cerrar el formulario o navegar a otro módulo.
- [ ] Temporizadores, tokens y componentes que requieren liberación se disponen correctamente.
- [ ] El intervalo de actualización y el procesamiento del ranking mantienen un consumo bajo y evitan sondeos agresivos.

#### Interfaz y seguridad

- [ ] La pantalla se adapta a los tamaños soportados por la ventana principal sin ocultar datos esenciales.
- [ ] El contenido extenso dispone de desplazamiento utilizable y comienza en una posición predecible.
- [ ] Los indicadores usan formatos y colores consistentes con los módulos existentes.
- [ ] No existen acciones para terminar procesos, cambiar prioridades, eliminar archivos o modificar Windows.
- [ ] No se recomienda finalizar procesos esenciales del sistema.

### Alcance posible con las mediciones actuales

La base existente permite implementar:

- ranking de RAM mediante `WorkingSetBytes`;
- porcentaje de RAM respecto a la memoria física total;
- ranking de CPU después de contar con dos muestras válidas;
- observaciones sobre la lectura actual;
- manejo de campos inaccesibles;
- actualización manual y periódica controlada;
- detalle del proceso seleccionado.

### Dependencias de la Fase 8

Los siguientes puntos **no deben presentarse como concluyentes en Fase 5**, porque requieren historial controlado:

- afirmar que el consumo es sostenido durante períodos largos;
- identificar patrones por hora, sesión o día;
- comparar el estado actual con una línea base histórica;
- detectar degradación progresiva;
- estimar frecuencia o recurrencia de picos;
- relacionar de manera temporal diferentes eventos de rendimiento.

Hasta la Fase 8, la aplicación puede conservar una ventana breve de muestras en memoria para estabilizar la visualización, pero debe denominarla observación reciente y no historial persistente.

## Fase 6 — Análisis preventivo de almacenamiento

- Analizar capacidad total, ocupación y espacio disponible.
- Identificar unidades con poco espacio mediante umbrales documentados.
- Explicar los posibles efectos del espacio insuficiente.
- Distinguir claramente ocupación de salud física: el espacio usado no permite diagnosticar desgaste o fallos del dispositivo.
- No eliminar archivos ni efectuar limpieza automática.

## Fase 7 — Motor de diagnóstico

Crear reglas verificables para detectar presión elevada de RAM, CPU elevada de manera sostenida, espacio insuficiente, procesos contribuyentes e información insuficiente.

Cada hallazgo deberá incluir:

1. problema u observación detectada;
2. evidencia utilizada;
3. posible impacto;
4. recomendación segura;
5. prioridad;
6. limitaciones de la conclusión.

Las reglas deben mantener separadas la observación, las posibles causas y la conclusión. Cuando la evidencia no alcance, el motor debe declararlo.

## Fase 8 — Historial de rendimiento

- Registrar mediciones durante períodos controlados.
- Identificar patrones y comparar momentos diferentes.
- Definir frecuencia de muestreo, retención y tamaño máximo.
- Permitir detener y eliminar el historial de forma controlada.
- Documentar qué datos se guardan, durante cuánto tiempo y dónde.
- Evitar recopilar información personal que no sea necesaria para el diagnóstico.

## Fase 9 — Reportes técnicos

- Exportar diagnósticos y su evidencia inicialmente a CSV.
- Evaluar PDF después de validar el contenido y la utilidad del reporte.
- Incluir fecha, alcance de la captura, unidades, valores no disponibles y limitaciones.
- Evitar incluir rutas, nombres de usuario u otros datos sensibles salvo necesidad explícita y consentimiento informado.

## Fase 10 — Integración y pruebas finales

Verificar estabilidad, rendimiento, diseño responsive, seguridad, navegación, consistencia visual, diagnósticos explicables y ausencia de recomendaciones peligrosas. Incluir pruebas de datos incompletos, procesos protegidos, cancelación, equipos con distintas capacidades y sesiones prolongadas.

## Condición para iniciar cada fase

Una fase debe comenzar con alcance y criterios verificables, reutilizar la arquitectura existente y terminar con validación funcional proporcional al riesgo. El paso a la fase siguiente no debe ocultar deuda técnica que afecte la exactitud de las mediciones o la seguridad de las recomendaciones.
