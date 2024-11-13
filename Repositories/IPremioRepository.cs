using EnergyApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPremioRepository
{
    /// <summary>
    /// Adiciona um novo prêmio ao banco de dados.
    /// </summary>
    Task<Premio> AdicionarAsync(Premio premio);

    /// <summary>
    /// Obtém um prêmio pelo seu ID.
    /// </summary>
    Task<Premio> ObterPorIdAsync(int id);

    /// <summary>
    /// Obtém todos os prêmios. Pode incluir ou excluir prêmios inativos.
    /// </summary>
    Task<IEnumerable<Premio>> ObterTodosAsync(bool incluirInativos = false);

    /// <summary>
    /// Atualiza os dados de um prêmio existente.
    /// </summary>
    Task<Premio> AtualizarAsync(Premio premio);

    /// <summary>
    /// Remove um prêmio pelo ID.
    /// </summary>
    Task<bool> DeletarAsync(int id);

    /// <summary>
    /// Obtém apenas prêmios que estão ativos.
    /// </summary>
    Task<IEnumerable<Premio>> ObterPremiosAtivosAsync();
}