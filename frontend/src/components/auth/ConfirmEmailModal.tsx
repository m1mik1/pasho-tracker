'use client';

import { Dialog, DialogContent, DialogHeader, DialogTitle } from '@/components/ui/dialog';
import { Button } from '@/components/ui/button';
import { CheckCircle } from 'lucide-react';
import { useRouter } from 'next/navigation';

export default function ConfirmEmailModal({ open, setOpen }: { open: boolean; setOpen: (open: boolean) => void }) {
  const router = useRouter();

  const handleClose = () => {
    setOpen(false);
    router.push('/?signin=true');
  };

  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogContent className="backdrop-blur-sm bg-white/90 border border-pink-200 max-w-md rounded-xl shadow-xl">
        <div className="flex flex-col items-center text-center space-y-4 p-4">
          <div className="flex items-center justify-center w-14 h-14 bg-pink-100 rounded-full">
            <CheckCircle className="w-7 h-7 text-pink-500" />
          </div>

          <DialogHeader>
            <DialogTitle className="text-2xl font-bold text-darkText">
              Almost there! 🎉
            </DialogTitle>
          </DialogHeader>

          <p className="text-gray-700 text-sm">
            We've sent a confirmation email to your inbox.
            Please verify to activate your account.
          </p>

          <Button 
            onClick={handleClose} 
            className="w-full bg-pink-500 hover:bg-pink-600 text-white font-semibold transition-all"
          >
            Got it!
          </Button>
        </div>
      </DialogContent>
    </Dialog>
  );
}
