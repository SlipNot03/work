<script setup>
import { useConcreteCalculator } from '../../composables/Calculator';

const emit = defineEmits(['requestCalculation']);
const { form, calculation } = useConcreteCalculator();
</script>

<template>
  <div class="calculator">
    <form>
      <label>
        Тип газобетона
        <select v-model.number="form.materialPrice">
          <option :value="6200">D400, от 6 200 руб./м3</option>
          <option :value="6450">D500, от 6 450 руб./м3</option>
          <option :value="6300">Перегородочный, от 6 300 руб./м3</option>
        </select>
      </label>
      <label>
        Длина дома, м
        <input v-model.number="form.length" type="number" min="1" step="0.5" />
      </label>
      <label>
        Ширина дома, м
        <input v-model.number="form.width" type="number" min="1" step="0.5" />
      </label>
      <label>
        Высота стен, м
        <input v-model.number="form.height" type="number" min="1" step="0.1" />
      </label>
      <label>
        Толщина блока, мм
        <select v-model.number="form.thickness">
          <option :value="100">100</option>
          <option :value="200">200</option>
          <option :value="300">300</option>
          <option :value="400">400</option>
        </select>
      </label>
      <label>
        Площадь проемов, м2
        <input v-model.number="form.openings" type="number" min="0" step="1" />
      </label>
      <label>
        Площадь перегородок, м2
        <input v-model.number="form.partitionArea" type="number" min="0" step="1" />
      </label>
      <label>
        Толщина перегородок, мм
        <select v-model.number="form.partitionThickness">
          <option :value="100">100</option>
          <option :value="150">150</option>
          <option :value="200">200</option>
        </select>
      </label>
      <label>
        Запас, %
        <input v-model.number="form.reserve" type="number" min="0" max="20" step="1" />
      </label>
      <label class="checkbox-field">
        <input v-model="form.includeGlue" type="checkbox" />
        <span>Учесть клей для газобетона</span>
      </label>
    </form>

    <aside>
      <div class="calculator-summary">
        <span>Расчет партии</span>
        <div>
          <strong>{{ calculation.pallets }}</strong>
          <small>паллет</small>
        </div>
        <p>Толщина блока {{ form.thickness }} мм, запас {{ form.reserve }}%</p>
      </div>
      <p>Предварительный объем блоков</p>
      <strong>{{ calculation.volume }} м3</strong>
      <ul>
        <li>
          <span>Площадь наружных стен</span>
          <strong>{{ calculation.outerWallArea }} м2</strong>
        </li>
        <li>
          <span>Объем наружных стен</span>
          <strong>{{ calculation.outerVolume }} м3</strong>
        </li>
        <li>
          <span>Объем перегородок</span>
          <strong>{{ calculation.partitionVolume }} м3</strong>
        </li>
        <li>
          <span>Ориентировочно паллет</span>
          <strong>{{ calculation.pallets }} шт.</strong>
        </li>
        <li>
          <span>Клей</span>
          <strong>{{ calculation.glue }} меш.</strong>
        </li>
        <li>
          <span>Стоимость материала</span>
          <strong>{{ calculation.materialCost }} руб.</strong>
        </li>
        <li>
          <span>Итого с клеем</span>
          <strong>{{ calculation.totalCost }} руб.</strong>
        </li>
      </ul>
      <small>
        Расчет не заменяет проект и служит для предварительной комплектации.
        Итоговые объем, цена и доставка подтверждаются менеджером.
      </small>
      <button type="button" @click="emit('requestCalculation')">
        Отправить расчет менеджеру
      </button>
    </aside>
  </div>
</template>
