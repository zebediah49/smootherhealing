using HarmonyLib;
using RimWorld;
using Verse;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SmootherHealing
{
	[StaticConstructorOnStartup]
	public static class Startup
	{
		static Startup()
		{
			new Harmony("zebediah49.SmootherHealing").PatchAll();
		}
	}

	[HarmonyPatch(typeof(Pawn_HealthTracker), "HealthTickInterval")]
	public static class Patch_HealthTickInterval
	{
		static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
		{
			foreach (var code in instructions)
			{
				//original line we're tampering with.  600 => 60
				//if ((pawn.RaceProps.IsFlesh || pawn.RaceProps.IsAnomalyEntity) && pawn.IsHashIntervalTick(600, delta) && (pawn.needs.food == null || !pawn.needs.food.Starving))
				if (code.opcode == OpCodes.Ldc_I4 && (int)code.operand == 600)
				{
					yield return new CodeInstruction(OpCodes.Ldc_I4, 60);
					continue;
				}

				//Original line we're tampering with.  0.01 => 0.001
				//tmpHediffInjuries.RandomElement().Heal(num2 * pawn.HealthScale * 0.01f * pawn.GetStatValue(StatDefOf.InjuryHealingFactor));
				if (code.opcode == OpCodes.Ldc_R4 && (float)code.operand == 0.01f)
				{
					yield return new CodeInstruction(OpCodes.Ldc_R4, 0.001f);
					continue;
				}

				yield return code;
			}
		}
	}
}

