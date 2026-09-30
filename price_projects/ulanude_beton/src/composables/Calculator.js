import { computed, reactive } from 'vue';

export function useConcreteCalculator() {
  const form = reactive({
    materialPrice: 6200,
    length: 10,
    width: 8,
    height: 3,
    thickness: 300,
    openings: 18,
    partitionArea: 24,
    partitionThickness: 100,
    includeGlue: true,
    reserve: 5,
  });

  const calculation = computed(() => {
    const perimeter = (Number(form.length) + Number(form.width)) * 2;
    const outerWallArea = Math.max(perimeter * Number(form.height) - Number(form.openings), 0);
    const outerVolume = outerWallArea * (Number(form.thickness) / 1000);
    const partitionVolume =
      Number(form.partitionArea) * (Number(form.partitionThickness) / 1000);
    const baseVolume = outerVolume + partitionVolume;
    const totalVolume = baseVolume * (1 + Number(form.reserve) / 100);
    const pallets = Math.ceil(totalVolume / 1.8);
    const glue = form.includeGlue ? Math.ceil(totalVolume * 1.2) : 0;
    const materialCost = Math.round(totalVolume * Number(form.materialPrice));
    const glueCost = glue * 420;

    return {
      outerWallArea: outerWallArea.toFixed(1),
      outerVolume: outerVolume.toFixed(2),
      partitionVolume: partitionVolume.toFixed(2),
      volume: totalVolume.toFixed(2),
      pallets,
      glue,
      materialCost: materialCost.toLocaleString('ru-RU'),
      totalCost: (materialCost + glueCost).toLocaleString('ru-RU'),
    };
  });

  return {
    form,
    calculation,
  };
}
