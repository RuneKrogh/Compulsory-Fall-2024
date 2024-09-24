import React from 'react';
import CustomerList from '../Lists/CustomerList.tsx';  // Import CustomerList
import OrderList from '../Lists/OrderList.tsx';
import PapersList from "../Lists/PapersList.tsx";
import PropertiesList from "../Lists/PropertiesList.tsx";        // Import OrderList

export default function AdminDashboard() {
    return (
        <div className="p-4">
            <h1 className="text-4xl font-bold mb-5 text-center">Admin Dashboard</h1>
            {/* Layout to display both lists */}
            <div className="grid grid-cols-1 lg:grid-cols-2 gap-4">
                {/* Customer List */}
                <div>
                    <CustomerList />
                </div>

                {/* Order List */}
                <div>
                    <OrderList />
                </div>
            </div>

            <div className="grid grid-cols-1 lg:grid-cols-2 gap-4">
                <div>
                    <PapersList/>
                </div>

                <div>
                    <PropertiesList/>
                </div>
            </div>
        </div>
    );
}
