using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Hero
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
         if (context.started) 
            hero.Jump();
      }

      public void OnSaySomething(InputAction.CallbackContext context)
      {
         if(context.canceled)
            hero.SaySomething();
      }
   }
}
