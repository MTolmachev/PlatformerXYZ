using UnityEngine;
using UnityEngine.InputSystem;

namespace Creatures.Character
{
   public class HeroInputReader : MonoBehaviour
   {
      private Hero hero;
      
      private void Awake()
      {
         hero = GetComponent<Hero>();
      }

      public void OnHorizontalMovement(InputAction.CallbackContext context)
      {
         var direction = context.ReadValue<Vector2>();
         hero.SetDirection(direction);
      }

      public void OnJump(InputAction.CallbackContext context)
      {
         if (context.performed) 
            hero.Jump();
      }

      public void OnInteract(InputAction.CallbackContext context)
      {
         if (context.performed)
            hero.Interact();
      }

      public void OnAttackInput(InputAction.CallbackContext context)
      {
         if (context.performed)
            hero.Attack();
      }
      
      public void OnThrow(InputAction.CallbackContext context)
      {
         if (context.performed)
         {
            hero.Throw();
         }
      }

      public void OnUsePotion(InputAction.CallbackContext context)
      {
         if (context.performed)
         {
            hero.UsePotion();
         }
      }
   }
}
