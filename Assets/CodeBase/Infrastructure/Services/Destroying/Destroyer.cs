using UnityEngine;

namespace Infrastructure.Services.Destroying
{
    public class Destroyer : IDestroyer
    {
        public void Destroy(GameObject gameObject) => 
            Object.Destroy(gameObject);
    }
}