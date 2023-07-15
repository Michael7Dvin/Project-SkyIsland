using UnityEngine;

namespace Infrastructure.Services.Destroying
{
    public interface IDestroyer
    {
        public void Destroy(GameObject gameObject);
    }
}