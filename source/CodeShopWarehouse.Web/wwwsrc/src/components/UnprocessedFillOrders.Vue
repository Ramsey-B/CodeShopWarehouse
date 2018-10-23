<template>
    <div>
        <h2>Create New FillOrder</h2>
        <form v-on:submit.prevent="createFillOrder">
            <input type="number" placeholder="product id" v-model="newFillOrder.productId">
            <input type="number" placeholder="stock" v-model="newFillOrder.stock">
            <button type="submit">submit</button>
        </form>

        <h2>Find FillOrders by Product Id</h2>
        <form v-on:submit.prevent="getFillOrderByProductId">
            <input type="number" placeholder="product Id" v-model="productId">
            <button type="submit">Submit</button>
        </form>

        <button @click="getUnprocessedFillOrders">Get All</button>
        <table>
            <tr>
                <th>ProductId</th>
                <th>Id</th>
                <th>Stock</th>
                <th>Created Date</th>
            </tr>
            <tr v-for="fillOrder in fillOrders" :key="fillOrder.id">
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
            newFillOrder: {
                stock: null,
                productId: null
            },
            productId: null
        }
    },
    mounted() {
        this.$store.dispatch("getUnprocessedFillOrders");
    },
    computed: {
        fillOrders() {
            return this.$store.state.fillOrders;
        }
    },
    methods: {
        processFillOrder(fillOrder) {
            this.$store.dispatch("processFillOrder", fillOrder);
        },
        createFillOrder() {
            this.$store.dispatch("createNewFillOrder", this.newFillOrder);
        },
        getFillOrderByProductId() {
            this.$store.dispatch("getFillOrdersByProductId", this.productId);
        },
        getUnprocessedFillOrders() {
            this.$store.dispatch("getUnprocessedFillOrders");
        }
    }
}
</script>

<style>

</style>
