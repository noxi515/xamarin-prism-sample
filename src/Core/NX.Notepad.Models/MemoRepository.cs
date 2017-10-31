using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NX.Notepad.Data;

namespace NX.Notepad
{
    public interface IMemoRepository
    {
        [NotNull, ItemCanBeNull]
        Task<Memo> GetAsync(Guid id);

        [NotNull, ItemNotNull]
        Task<List<Memo>> GetAllAsync();

        [NotNull]
        Task AddOrUpdateAsync([NotNull] Memo memo);

        [NotNull]
        Task<bool> DeleteAsync(Guid id);
    }

    public class MemoRepository : IMemoRepository
    {
        private readonly IDictionary<Guid, Memo> _memos = new Dictionary<Guid, Memo>();

        public Task<Memo> GetAsync(Guid id)
        {
            return Task.FromResult(_memos.TryGetValue(id, out var memo) ? memo : null);
        }

        public Task<List<Memo>> GetAllAsync()
        {
            return Task.FromResult(_memos.Values.ToList());
        }

        public Task AddOrUpdateAsync(Memo memo)
        {
            return Task.Run(() =>
            {
                if (memo.Id == null)
                {
                    memo.Id = GenerateNewId();
                }

                _memos[memo.Id.Value] = memo;
            });
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            return Task.FromResult(_memos.Remove(id));
        }

        private Guid GenerateNewId()
        {
            while (true)
            {
                var id = Guid.NewGuid();
                if (!_memos.ContainsKey(id))
                {
                    return id;
                }
            }
        }
    }
}