<template>
    <div>
        <h2>Get FillOrder By Id</h2>
        <form v-on:submit.prevent="getFillOrderById">
            <input type="number" placeholder="FillOrder Id" v-model="fillOrderId">
            <button type="submit">Submit</button>
        </form>

        <table>
            <tr>
                <th>ProductId</th>
                <th>Id</th>
                <th>Stock</th>
                <th>Created Date</th>
            </tr>
            <tr>
                <td>{{fillOrder.productId}}</td>
                <td>{{fillOrder.id}}</td>
                <td>{{fillOrder.stock}}</td>
                <td>{{fillOrder.createdDate}}</td>
                <td v-if="!fillOrder.processed"><button @click="processFillOrder(fillOrder)">Process</button></td>
            </tr>
        </table>
    </div>
</template>

<script>
export default {
    data() {
        return {
            fillOrderId: null
        }
    },
    computed: {
        fillOrder() {
            return this.$store.state.fillOrder;
        }
    },
    methods: {
        getFillOrderById() {
            this.$store.dispatch("getFillOrderById", this.fillOrderId);
        },
        processFillOrder(fillOrder) {
            this.$store.dispatch("processFillOrder", fillOrder);
        }
    }
}
</script>

<style>

</style>
