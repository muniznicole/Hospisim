using Hospisim.Models;
using Hospisim.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Hospisim.Data
{
    public static class DbInitializer
    {
        public static void Initialize(HospisimContext context)
        {
            context.Database.EnsureCreated();

            if (context.Pacientes.Any()) return;

            var especialidades = new[]
            {
                new Especialidade { Id = Guid.NewGuid(), Nome = "Clínico Geral" },
                new Especialidade { Id = Guid.NewGuid(), Nome = "Pediatria" },
                new Especialidade { Id = Guid.NewGuid(), Nome = "Ortopedia" },
                new Especialidade { Id = Guid.NewGuid(), Nome = "Cardiologia" },
                new Especialidade { Id = Guid.NewGuid(), Nome = "Dermatologia" },
                new Especialidade { Id = Guid.NewGuid(), Nome = "Neurologia" },
                new Especialidade { Id = Guid.NewGuid(), Nome = "Ginecologia" },
                new Especialidade { Id = Guid.NewGuid(), Nome = "Endocrinologia" },
                new Especialidade { Id = Guid.NewGuid(), Nome = "Oftalmologia" },
                new Especialidade { Id = Guid.NewGuid(), Nome = "Psiquiatria" }
            };
            context.Especialidades.AddRange(especialidades);

            var profissionais = especialidades.Select((esp, i) => new Profissional
            {
                Id = Guid.NewGuid(),
                NomeCompleto = $"Dr. Especialista {i + 1}",
                EspecialidadeId = esp.Id,
                CPF = $"111111111{i:00}",
                Email = $"especialista{i}@hospital.com",
                Telefone = $"(63) 91111-000{i}",
                RegistroConselho = $"CRM-{i + 1000}",
                TipoRegistro = "CRM",
                DataAdmissao = DateTime.Today.AddYears(-2).AddDays(i * 30),
                CargaHorariaSemanal = 40,
                Turno = i % 2 == 0 ? "Diurno" : "Noturno",
                Ativo = true
            }).ToArray();
            context.Profissionais.AddRange(profissionais);

            var pacientes = Enumerable.Range(1, 10).Select(i => new Paciente
            {
                Id = Guid.NewGuid(),
                NomeCompleto = $"Paciente Exemplo {i}",
                DataNascimento = DateTime.Today.AddYears(-20 - i),
                CPF = $"000000000{i:00}",
                Telefone = $"(63) 90000-000{i}",
                Email = $"paciente{i}@email.com",
                Sexo = (Sexo)(i % 3),
                TipoSanguineo = (TipoSanguineo)(i % 8),
                EstadoCivil = (EstadoCivil)(i % 4),
                EnderecoCompleto = $"Rua Exemplo {i}, Centro, Palmas-TO",
                NumeroCartaoSUS = $"12345678900{i}",
                PossuiPlanoSaude = i % 2 == 0
            }).ToArray();
            context.Pacientes.AddRange(pacientes);

            var prontuarios = pacientes.Select(p => new Prontuario
            {
                Id = Guid.NewGuid(),
                Numero = Guid.NewGuid().ToString()[..8].ToUpper(),
                PacienteId = p.Id,
                DataAbertura = DateTime.Now.AddDays(-10),
                ObservacoesGerais = "Sem observações adicionais no momento."
            }).ToArray();
            context.Prontuarios.AddRange(prontuarios);

            var atendimentos = prontuarios.Select((pr, i) => new Atendimento
            {
                Id = Guid.NewGuid(),
                DataHora = DateTime.Now.AddHours(-i),
                Tipo = i % 3 == 0 ? TipoAtendimento.Internacao : (i % 2 == 0 ? TipoAtendimento.Emergencia : TipoAtendimento.Consulta),
                Status = StatusAtendimento.EmAndamento,
                Local = $"Sala {i + 1}",
                ProntuarioId = pr.Id,
                ProfissionalId = profissionais[i % profissionais.Length].Id
            }).ToArray();
            context.Atendimentos.AddRange(atendimentos);

            var prescricoes = atendimentos.Select((at, i) => new Prescricao
            {
                Id = Guid.NewGuid(),
                AtendimentoId = at.Id,
                ProfissionalId = at.ProfissionalId,
                Medicamento = $"Medicamento {i + 1}",
                Dosagem = "10mg",
                Frequencia = "2x ao dia",
                ViaAdministracao = "Oral",
                StatusPrescricao = StatusPrescricao.Ativa,
                Observacoes = "Tomar após refeições",
                DataInicio = DateTime.Today,
                DataFim = DateTime.Today.AddDays(5),
                ReacoesAdversas = i % 3 == 0 ? "Náusea" : null
            }).ToArray();
            context.Prescricoes.AddRange(prescricoes);

            var exames = atendimentos.Select((at, i) => new Exame
            {
                Id = Guid.NewGuid(),
                AtendimentoId = at.Id,
                Tipo = i % 2 == 0 ? "Hemograma" : "Raio-X",
                DataSolicitacao = DateTime.Now.AddDays(-i),
                Resultado = "Resultado pendente",
                StatusExame = StatusExame.Solicitado
            }).ToArray();
            context.Exames.AddRange(exames);

            var prontuarioPacienteMap = prontuarios.ToDictionary(p => p.Id, p => p.PacienteId);
            var internacoes = atendimentos
                .Where(a => a.Tipo == TipoAtendimento.Internacao)
                .Select((at, i) => new Internacao
                {
                    Id = Guid.NewGuid(),
                    AtendimentoId = at.Id,
                    PacienteId = prontuarioPacienteMap[at.ProntuarioId],
                    DataEntrada = at.DataHora,
                    PrevisaoAlta = DateTime.Now.AddDays(5 + i),
                    Setor = "Clínico Geral",
                    Quarto = (101 + i).ToString(),
                    Leito = (1 + i).ToString(),
                    MotivoInternacao = "Observação médica",
                    PlanoSaudeUtilizado = "Plano Saúde Vida",
                    ObservacoesClinicas = "Estável",
                    StatusInternacao = StatusInternacao.Ativa
                }).ToArray();
            context.Internacoes.AddRange(internacoes);

            var altas = internacoes.Take(5).Select(i => new AltaHospitalar
            {
                Id = Guid.NewGuid(),
                InternacaoId = i.Id,
                Data = i.DataEntrada.AddDays(3),
                CondicaoPaciente = "Recuperado",
                InstrucoesPosAlta = "Manter repouso e retornar em 15 dias"
            }).ToArray();
            context.AltasHospitalares.AddRange(altas);

            context.SaveChanges();
        }
    }
}
