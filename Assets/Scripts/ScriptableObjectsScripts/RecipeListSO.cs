using UnityEngine;
using System.Collections.Generic;

//[CreateAssetMenu ()]              ....made sure no it is singleton no other list of recipies is made in game
public class RecipeListSO : ScriptableObject
{
    public List<RecipeSO> waitingRecipeList;
}
