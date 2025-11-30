# 📘 Introducción

Este documento define la arquitectura conceptual, el modelo de dominio, los bounded contexts, los agregados, las entidades y las reglas fundamentales del **Sistema Financiero con Workspaces**, una plataforma diseñada para gestionar finanzas personales, compartidas y empresariales de forma modular, escalable y segura.

El sistema permite que múltiples usuarios colaboren dentro de espacios financieros aislados llamados **Workspaces**, donde cada uno posee cuentas, transacciones, categorías, fuentes, reglas recurrentes y facturas procesadas mediante IA. Esta arquitectura implementa **Domain-Driven Design (DDD)** para asegurar una estructura clara, extensible y orientada al negocio.

El propósito del sistema es ofrecer:

* Gestión financiera personal o compartida
* Aislamiento completo por workspace (multi-tenant)
* Colaboración basada en roles y permisos
* Registro y clasificación de transacciones
* Cuentas con balance automático
* Soporte para reglas recurrentes
* Integración de facturas con procesamiento mediante IA
* Dominio robusto, desacoplado, mantenible y preparado para escalabilidad

Este documento reúne toda la base teórica y técnica necesaria para dar vida al backend del sistema utilizando **DDD + .NET 9**.

# 📘 Diseño del Dominio — Sistema Financiero con Workspaces (DDD + .NET 9)

Este documento formaliza todo lo discutido respecto al **modelo de dominio**, **agregados**, **entities**, **bounded contexts**, **value objects**, **motivos de cada PK**, y la arquitectura general basada en **DDD** para el Sistema Financiero con Workspaces.

Cada sección incluye:

* Explicación detallada
* Razón de diseño
* Código sugerido
* Enlaces a diagramas
* Código del diagrama (PlantUML / ERD)

> ⚠️ *Nota:* Los diagramas se incluyen como **enlace + código PlantUML** para que puedas pegarlos directamente donde los necesites.

---

# 📍 1. Dominio y Subdominios

## ✔ Dominio principal

El dominio del negocio es:
**Gestión colaborativa de finanzas personales o compartidas**, organizada por Workspaces, con soporte para permisos, cuentas, transacciones, categorías, fuentes, reglas recurrentes e integración con facturas.

## ✔ Subdominios

### 1. Core Subdomain — *Finance*

Donde vive la lógica principal del negocio:

* Accounts
* Transactions
* Categories
* Sources
* Recurrence Rules
* Balance logic

### 2. Supporting Subdomain — *Workspace Management*

* Workspaces
* Workspace members
* Roles & permisos

### 3. Supporting Subdomain — *Billing*

* Invoices
* Metadata
* Invoice → Transaction link

### 4. Generic Subdomain — *Identity Access*

* User
* Authentication details

---

# 📍 2. Bounded Contexts

* IdentityAccessContext
* WorkspaceContext
* FinanceContext (Core)
* BillingContext

Cada contexto encapsula un modelo distinto.

---

# 📍 3. Aggregate Roots y Entities

A continuación se listan todos los agregados, cada entidad, su PK y el motivo.

---

# 🏛️ 3.1 IdentityAccessContext

## **Entity: User (Aggregate Root)**

### **Primary Key:** `Guid Id`

### **Motivo:**

Porque cada usuario tiene identidad única e independiente del resto del sistema. Su ciclo de vida no depende de ningún otro objeto.

### **Función:**

Representa al usuario del sistema, controla su autenticación y estado.

---

# 🏢 3.2 WorkspaceContext

## **Aggregate Root: Workspace**

### **Primary Key:** `Guid Id`

**Motivo:** Un Workspace es el contenedor natural del resto de objetos (accounts, categories, etc.) por motivos de aislamiento y multitenancy.

### **Función:**

Aloja cuentas, transacciones, roles, permisos, fuentes y reglas recurrentes.

## **Entity interna: WorkspaceMember**

### **Primary Key:** `Guid Id`

**Motivo:** Se requiere un ID único para permitir auditoría y referencias explícitas si el sistema crece.

### **Función:**

Determina el rol y permisos de cada usuario dentro de un Workspace.

---

# 💰 3.3 FinanceContext (Core Domain)

## **Aggregate Root: Account**

### **Primary Key:** `Guid Id`

**Motivo:** Las cuentas deben tener vida propia, y su identidad no depende de transacciones.

### **Función:**

Representa cuentas financieras (banco, tarjeta, efectivo). Controla el saldo.

---

## **Aggregate Root: Transaction**

### **Primary Key:** `Guid Id`

**Motivo:** Cada transacción debe poder identificarse, modificarse, enlazarse a facturas y auditarse.

### **Función:**

Registrar ingresos o gastos.

---

## **Aggregate Root: Category**

### **Primary Key:** `Guid Id`

**Motivo:** Una categoría debe ser compartible, jerárquica y reutilizable.

### **Función:**

Clasificar ingresos/gastos.

---

## **Aggregate Root: Source**

### **Primary Key:** `Guid Id`

**Motivo:** El origen del gasto/ingreso debe ser independiente y reutilizable.

### **Función:**

Identificar proveedores, clientes o lugares.

---

## **Aggregate Root: RecurrenceRule**

### **Primary Key:** `Guid Id`

**Motivo:** Permite configurar recurrencias de forma autónoma.

### **Función:**

Generar transacciones periódicas.

---

# 🧾 3.4 BillingContext

## **Aggregate Root: Invoice**

### **Primary Key:** `Guid Id`

**Motivo:** Una factura debe ser única, procesable y enlazable a múltiples transacciones.

### **Función:**

Representa una factura y los datos procesados por IA.

---

# 📍 4. Value Objects

Value Objects usados globalmente:

* `Money`
* `Email`
* `DateRange`

Cada uno sin identidad, con igualdad estructural.

---

# 📍 5. Eventos de Dominio

Eventos que permiten comunicación entre contextos.

* `TransactionCreated`
* `RecurrenceRuleTriggered`
* `InvoiceProcessed`
* `WorkspaceMemberAdded`
* `UserStatusChanged`

---

# 📍 6. Servicios de Dominio

* AuthorizationService
* WorkspacePermissionService
* BalanceService
* RecurrenceExecutionService
* InvoiceProcessingService

---

# 📍 7. ERD — Enlace y Código

### **🔗 Enlace recomendado para visualizar:**

[https://www.plantuml.com/plantuml](https://www.plantuml.com/plantuml)

### **Código del ERD:**

<img width="1367" height="593" alt="image" src="https://github.com/user-attachments/assets/edd9c13d-e4bf-453b-adc3-14be27bfbe6e" />


---

# 📍 8. Context Map — Enlace y Código

### **🔗 Enlace:**

[https://www.plantuml.com/plantuml](https://www.plantuml.com/plantuml)

### **Código:**

<img width="695" height="575" alt="image" src="https://github.com/user-attachments/assets/bcc2f16f-7d8a-47bc-ac09-0322863c3106" />


---

# 📍 9. Código del Dominio (C# / .NET 9)

Incluye:

* Building blocks
* Entities por contexto
* Aggregate roots
* Value objects


# 📍 10. Conclusión

Este documento consolida:

* Modelo de dominio
* Bounded contexts
* Agregados y entidades
* Eventos
* Value objects
* Motivo de cada PK
* Diagramas en UML
* Código base para .NET 9
