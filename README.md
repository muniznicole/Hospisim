# HOSPISIM - Sistema de Atendimento Hospitalar

## Visão Geral do Projeto

O **Hospisim** é um sistema de gestão hospitalar desenvolvido em ASP.NET Core MVC com Entity Framework Core e SQL Server. Seu objetivo é simular o ciclo completo de atendimento hospitalar, desde a criação de prontuários e registros de pacientes, até a evolução do atendimento com prescrições, exames, internações e altas hospitalares.

## Regras de Negócio

1. **Cadastro de Pacientes**:
   - Pacientes possuem informações completas como CPF, nome, data de nascimento, tipo sanguíneo, estado civil, etc.

2. **Prontuários**:
   - Cada paciente possui um ou mais prontuários com histórico de atendimentos.

3. **Atendimentos**:
   - Um atendimento é iniciado com tipo *Consulta* e status *Em Andamento*.
   - Pode evoluir para *Emergência* ou *Internação*.
   - Apenas *Consultas* e *Emergências* podem ser finalizadas diretamente.
   - *Internações* exigem alta hospitalar para finalização do atendimento.

4. **Prescrições e Exames**:
   - Vinculados ao atendimento, com dados como medicamento, via de administração e resultado de exames.

5. **Internação**:
   - Possui dados de quarto, leito, motivo, observações clínicas e previsão de alta.
   - Relacionada diretamente ao paciente e ao atendimento.

6. **Alta Hospitalar**:
   - Pode ser concedida apenas para internações ativas.
   - Atualiza o status da internação e finaliza o atendimento.

## Diagrama de Entidades (Descrição Textual)

- **Paciente** 1:N **Prontuario**
- **Prontuario** 1:N **Atendimento**
- **Atendimento** 1:N **Prescricao**
- **Atendimento** 1:N **Exame**
- **Atendimento** 0:1 **Internacao**
- **Internacao** 1:1 **AltaHospitalar**'
- **Atendimento** N:1 **Profissional**

### Resumo:

Um paciente possui vários prontuários, cada prontuário pode ter vários atendimentos. Atendimentos podem conter várias prescrições e exames, podem evoluir para internação (um para um), e internações podem receber alta hospitalar.

## Instruções de Execução do Projeto

1. **Requisitos**:
   - .NET 7 SDK
   - SQL Server Management Studio
   - Visual Studio 2022 ou superior

2. **Clone o repositório**:
   ```bash
   git clone https://github.com/muniznicole/Hospisim.git
   cd Hospisim
   ```

3. **Configure a string de conexão**:
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

Isso criará o banco de dados e populá-lo-á automaticamente com 10 registros de cada entidade através do método `DbInitializer`.

### 5. Executar o Projeto

- Pressione `Ctrl + F5` no Visual Studio

### 6. Acesso

Acesse via navegador:

- `https://localhost:44361` (utilizei este endereço)
