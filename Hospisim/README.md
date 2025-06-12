# HOSPISIM - Sistema de Atendimento Hospitalar

## Vis�o Geral do Projeto

O **Hospisim** � um sistema de gest�o hospitalar desenvolvido em ASP.NET Core MVC com Entity Framework Core e SQL Server. Seu objetivo � simular o ciclo completo de atendimento hospitalar, desde a cria��o de prontu�rios e registros de pacientes, at� a evolu��o do atendimento com prescri��es, exames, interna��es e altas hospitalares.

## Regras de Neg�cio

1. **Cadastro de Pacientes**:
   - Pacientes possuem informa��es completas como CPF, nome, data de nascimento, tipo sangu�neo, estado civil, etc.

2. **Prontu�rios**:
   - Cada paciente possui um ou mais prontu�rios com hist�rico de atendimentos.

3. **Atendimentos**:
   - Um atendimento � iniciado com tipo *Consulta* e status *Em Andamento*.
   - Pode evoluir para *Emerg�ncia* ou *Interna��o*.
   - Apenas *Consultas* e *Emerg�ncias* podem ser finalizadas diretamente.
   - *Interna��es* exigem alta hospitalar para finaliza��o do atendimento.

4. **Prescri��es e Exames**:
   - Vinculados ao atendimento, com dados como medicamento, via de administra��o e resultado de exames.

5. **Interna��o**:
   - Possui dados de quarto, leito, motivo, observa��es cl�nicas e previs�o de alta.
   - Relacionada diretamente ao paciente e ao atendimento.

6. **Alta Hospitalar**:
   - Pode ser concedida apenas para interna��es ativas.
   - Atualiza o status da interna��o e finaliza o atendimento.

## Diagrama de Entidades (Descri��o Textual)

- **Paciente** 1:N **Prontuario**
- **Prontuario** 1:N **Atendimento**
- **Atendimento** 1:N **Prescricao**
- **Atendimento** 1:N **Exame**
- **Atendimento** 0:1 **Internacao**
- **Internacao** 1:1 **AltaHospitalar**'
- **Atendimento** N:1 **Profissional**

### Resumo:

Um paciente possui v�rios prontu�rios, cada prontu�rio pode ter v�rios atendimentos. Atendimentos podem conter v�rias prescri��es e exames, podem evoluir para interna��o (um para um), e interna��es podem receber alta hospitalar.

## Instru��es de Execu��o do Projeto

1. **Requisitos**:
   - .NET 7 SDK
   - SQL Server Management Studio
   - Visual Studio 2022 ou superior

2. **Clone o reposit�rio**:
   ```bash
   git clone https://github.com/muniznicole/Hospisim.git
   cd Hospisim
   ```

3. **Configure a string de conex�o**:
   - No arquivo `appsettings.json`, edite:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=HospisimDb;Trusted_Connection=True;MultipleActiveResultSets=true"
     }
     ```
### 4. Aplicar Migrations e Popular o Banco de Dados

No Visual Studio, abra o **Package Manager Console** e execute:

```powershell
Update-Database
```

Isso criar� o banco de dados e popul�-lo-� automaticamente com 10 registros de cada entidade atrav�s do m�todo `DbInitializer`.

### 5. Executar o Projeto

- Pressione `Ctrl + F5` no Visual Studio

### 6. Acesso

Acesse via navegador:

- `https://localhost:44361` (utilizei este endere�o)
